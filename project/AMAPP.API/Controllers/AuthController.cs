using AMAPP.API.DTOs;
using AMAPP.API.DTOs.Auth;
using AMAPP.API.Models;
using AMAPP.API.Services.Implementations;
using AMAPP.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace AMAPP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IEmailService emailService, IConfiguration configuration, TokenService tokenService, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;

            _emailService = emailService;
            _configuration = configuration;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterUserRequestDto registerUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (registerUser.Password != registerUser.ConfirmPassword)
            {
                return BadRequest("Password not equal.");
            }

            var verify = await _userManager.FindByEmailAsync(registerUser.Email);

            if (verify != default)
            {
                return BadRequest("User email is already registered.");
            }

            var newUser = new User
            {
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                UserName = registerUser.Email,
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(newUser, registerUser.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(newUser, "ADMIN");

            return Created("", newUser.Email);
        }


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Tentativa de login com dados inválidos para email");
                    return BadRequest("Bad credentials");
                }
                var user = await _userManager.FindByEmailAsync(loginRequest.Email);

                if (user != null && await _userManager.CheckPasswordAsync(user, loginRequest.Password))
                {
                    // Verificar se conta está bloqueada
                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        _logger.LogWarning("Tentativa de login em conta bloqueada: {UserId}", user.Id);
                        return Unauthorized("Conta temporariamente bloqueada devido a tentativas falhadas");
                    }

                    if(await _userManager.GetTwoFactorEnabledAsync(user))
                        return await GenerateOTPFor2FactorAuthentication(user);

                    var roles = await _userManager.GetRolesAsync(user);
                    var token = _tokenService.GenerateToken(user, roles.ToList());

                    var expirationMinutes = _configuration.GetValue<int>("JwtSettings:ExpiryMinutes", 60);
                    var loginResponse = new LoginResponseDto
                    {
                        Token = token,
                        Expiration = DateTime.UtcNow.AddMinutes(expirationMinutes)
                    };

                    _logger.LogInformation("Login bem-sucedido para utilizador: {UserId}",
                        user.Id);

                    return Ok(loginResponse);
                }

                _logger.LogWarning("Tentativa de login falhada de IP: {IP}",
                    HttpContext.Connection.RemoteIpAddress);

                return Unauthorized("Invalid Authentication");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante login");
                return StatusCode(500, "Erro interno do servidor");
            }
            
        }

        private async Task<IActionResult> GenerateOTPFor2FactorAuthentication(User user)
        {
            var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);
            if (!providers.Contains("Email"))
                return Unauthorized("Invalid 2-Factor Provider.");

            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            if(user.Email == null)
            {
                return BadRequest("Email não configurado");
            }

            var message = new MessageDto(user.Email, "Authentication token", token);

            await _emailService.SendEmailAsync(message);

            return Ok(new LoginResponseDto { Is2FactorRequired = true, Provider = "Email" });
        }

        [HttpPost("twofactor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> TwoFactor(TwoFactorRequestDto twoFactorRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _userManager.FindByEmailAsync(twoFactorRequest.Email!);
                if (user == null)
                {
                    return BadRequest("Invalid Request");
                }

                var validVerification = await _userManager.VerifyTwoFactorTokenAsync(user, twoFactorRequest.Provider!, twoFactorRequest.Token!);

                var roles = await _userManager.GetRolesAsync(user);
                var token = _tokenService.GenerateToken(user, roles.ToList());

                var expirationMinutes = _configuration.GetValue<int>("JwtSettings:ExpiryMinutes", 60);
                var loginResponse = new LoginResponseDto
                {
                    Token = token,
                    Expiration = DateTime.UtcNow.AddMinutes(expirationMinutes)
                };

                _logger.LogInformation("Login bem-sucedido para utilizador: {UserId}",
                    user.Id);

                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante login");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].FirstOrDefault();

                if (authHeader == null || !authHeader.StartsWith("Bearer "))
                {
                    return BadRequest("Invalid authorization header");
                }

                var token = authHeader.Substring("Bearer ".Length).Trim();
                var jti = _tokenService.ExtractJtiFromTokenAsync(token);

                if (string.IsNullOrEmpty(jti))
                {
                    return BadRequest("Invalid token");
                }

                // Extrair expiração do token para definir quando remover da blacklist
                var tokenHandler = new JwtSecurityTokenHandler();
                var jsonToken = tokenHandler.ReadJwtToken(token);
                var expiration = jsonToken.ValidTo;

                // Adicionar token à blacklist
                var blacklistService = HttpContext.RequestServices.GetRequiredService<ITokenBlacklistService>();
                await blacklistService.RevokeTokenAsync(jti, expiration);

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                _logger.LogInformation("Logout realizado com sucesso para UserId: {UserId}, Token JTI: {JTI}",
                    userId, jti);

                return Ok(new { message = "Logout realizado com sucesso" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante logout");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpGet("confirmemail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConfirmEmail(string userEmail, string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userEmail) || string.IsNullOrWhiteSpace(token))
                {
                    return BadRequest("Email e token são obrigatórios");
                }

                var user = await _userManager.FindByEmailAsync(userEmail);

                if (user == default)
                {
                    _logger.LogWarning("Tentativa de confirmação de email para utilizador inexistente");
                    return NotFound("Utilizador não encontrado");
                }

                var decodedToken = WebEncoders.Base64UrlDecode(token);
                string normalToken = Encoding.UTF8.GetString(decodedToken);


                var result = await _userManager.ConfirmEmailAsync(user, normalToken);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Falha na confirmação de email para utilizador: {UserId}", user.Id);
                    return BadRequest("Token inválido ou expirado");
                }

                _logger.LogInformation("Email confirmado com sucesso para utilizador: {UserId}", user.Id);
                return Ok("Email confirmado com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante confirmação de email");
                return StatusCode(500, "Erro interno do servidor");
            }
            
        }

        [HttpPost("change-password")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound("Utilizador não encontrado");
                }

                var result = await _userManager.ChangePasswordAsync(user,
                    changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Falha na mudança de password para utilizador: {UserId}", user.Id);
                    return BadRequest(result.Errors.Select(e => e.Description));
                }

                _logger.LogInformation("Password alterada com sucesso para utilizador: {UserId}", user.Id);

                return Ok(new { Message = "Password alterada com sucesso" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante mudança de password");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpGet("forgetpassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == default)
            {
                return NotFound();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedEmailToken = Encoding.UTF8.GetBytes(token);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
            //Url Front-end page for password reset
            var url = $"{_configuration["AppUrl"]}/resetemail?useremail={email}&token={validEmailToken}";

            string subject = "Confirm your email account";
            string body = $"<p>To reset your password <a href='{url}'>Click here</a></p>";

            await _emailService.SendEmailAsync(new MessageDto(email, subject, body));

            return Ok();
        }

        [HttpPost("resetemail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ResetEmail(ResetEmailRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!request.Password.Equals(request.ConfirmPassword))
                {
                    return BadRequest("The password's are not the same");
                }

                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == default)
                {
                    return NotFound("Utilizador não encontrado");
                }

                var decodedToken = WebEncoders.Base64UrlDecode(request.Token);
                string normalToken = Encoding.UTF8.GetString(decodedToken);

                var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

                if (!result.Succeeded)
                {
                    _logger.LogWarning("Falha no reset de password para utilizador: {UserId}", user.Id);
                    return BadRequest();
                }

                _logger.LogInformation("Password redefinida com sucesso para utilizador: {UserId}", user.Id);
                return Ok("Password redefinida com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante reset de password");
                return StatusCode(500, "Erro interno do servidor");
            }
           
        }
    }
}
