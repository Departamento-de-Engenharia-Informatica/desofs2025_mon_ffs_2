using AMAPP.API.Configurations;
using AMAPP.API.DTOs.Auth;
using AMAPP.API.Models;
using AMAPP.API.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AMAPP.API.Services.Implementations;

public class TokenService
{

    private readonly JwtSettings _jwtSettings;
    private readonly ILogger<TokenService> _logger;
    private readonly ITokenBlacklistService _blacklistService;

    public TokenService(IOptions<JwtSettings> jwtSettings, ILogger<TokenService> logger, ITokenBlacklistService blacklistService)
    {
        _jwtSettings = jwtSettings.Value;
        _logger = logger;
        _blacklistService = blacklistService;
    }

    public string GenerateToken(User user, List<string>? roles = null)
    {
        try
        {

            if (user == null)
            {
                _logger.LogError("Tentativa de gerar token com user null");
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(user.Id))
            {
                _logger.LogError("Tentativa de gerar token com user.Id vazio");
                throw new ArgumentException("User.Id não pode estar vazio", nameof(user));
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                _logger.LogError("Tentativa de gerar token com user.Email vazio para UserId: {UserId}", user.Id);
                throw new ArgumentException("User.Email não pode estar vazio", nameof(user));
            }

            // Gerar JTI único para este token
            var jti = Guid.NewGuid().ToString();
            var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);

            // Criar claims com validação
            var claims = CreateClaims(user, roles, jti);

            // Get secret key from configuration
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = credentials,
                NotBefore = DateTime.UtcNow
            };


            // Return the serialized token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Log de segurança (sem dados sensíveis)
            _logger.LogInformation("Token JWT gerado com sucesso para UserId: {UserId}, Roles: [{Roles}], Expira: {Expiration}",
                user.Id,
                roles != null ? string.Join(", ", roles) : "Nenhuma",
                tokenDescriptor.Expires);

            return tokenString;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao gerar token para UserId: {UserId}", user?.Id);
            throw;
        }
    }

    public async Task<bool> ValidateToken(string token)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                _logger.LogWarning("Tentativa de validação de token vazio ou null");
                return false;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero // Sem tolerância de tempo
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

            // Extrair JTI do token
            var jti = principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

            if (string.IsNullOrEmpty(jti))
            {
                _logger.LogWarning("Token sem JTI recebido");
                return false;
            }

            // Verificar se o token está na blacklist
            var isRevoked = await _blacklistService.IsTokenRevokedAsync(jti);
            if (isRevoked)
            {
                _logger.LogWarning("Token revogado tentou ser usado: {JTI}", jti);
                return false;
            }

            // Verificar se é um JWT válido
            if (validatedToken is not JwtSecurityToken jwtToken ||
                !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                _logger.LogWarning("Token com algoritmo inválido recebido");
                return false;
            }

            _logger.LogDebug("Token validado com sucesso");
            return true;
        }
        catch (SecurityTokenException ex)
        {
            _logger.LogWarning("Token inválido: {Error}", ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro durante validação de token");
            return false;
        }
    }

    public string? ExtractJtiFromTokenAsync(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadJwtToken(token);
            return jsonToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao extrair JTI do token");
            return null;
        }
    }

    public async Task<ClaimsPrincipal?> GetPrincipalFromToken(string token)
    {
        try
        {
            if (!await ValidateToken(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false, // Não validar expiração para refresh
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            return principal;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao extrair principal do token");
            return null;
        }
    }

    private List<Claim> CreateClaims(User user, List<string>? roles, string jti)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, jti), // ID único do token
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Name, user.UserName ?? user.Email!),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
            new("FirstName", user.FirstName ?? ""),
            new("LastName", user.LastName ?? ""),
            new("iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

        // Adicionar roles com validação
        if (roles != null && roles.Count > 0)
        {
            foreach (var role in roles.Where(r => !string.IsNullOrWhiteSpace(r)))
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Trim()));
            }

            _logger.LogDebug("Adicionadas {RoleCount} roles ao token para UserId: {UserId}",
                roles.Count, user.Id);
        }
        else
        {
            _logger.LogWarning("Token gerado sem roles para UserId: {UserId}", user.Id);
        }

        return claims;
    }
}
