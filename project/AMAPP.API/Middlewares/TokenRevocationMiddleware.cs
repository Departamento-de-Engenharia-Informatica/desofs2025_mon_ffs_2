using AMAPP.API.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace AMAPP.API.Middlewares
{
    public class TokenRevocationMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TokenRevocationMiddleware> _logger;

        public TokenRevocationMiddleware(
            RequestDelegate next,
            IServiceProvider serviceProvider,
            ILogger<TokenRevocationMiddleware> logger)
        {
            _next = next;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Pular verificação para endpoints que não precisam de autenticação
            if (ShouldSkipValidation(context))
            {
                await _next(context);
                return;
            }

            var token = ExtractToken(context);

            if (!string.IsNullOrEmpty(token))
            {
                using var scope = _serviceProvider.CreateScope();
                var blacklistService = scope.ServiceProvider.GetRequiredService<ITokenBlacklistService>();

                var jti = ExtractJtiFromToken(token);

                if (!string.IsNullOrEmpty(jti))
                {
                    var isRevoked = await blacklistService.IsTokenRevokedAsync(jti);

                    if (isRevoked)
                    {
                        _logger.LogWarning("Token revogado detectado: {JTI} de IP: {IP}",
                            jti, context.Connection.RemoteIpAddress);

                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Token has been revoked");
                        return;
                    }
                }
            }

            await _next(context);
        }

        private bool ShouldSkipValidation(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower() ?? "";

            // Endpoints que não precisam de verificação de revogação
            var skipPaths = new[]
            {
                "/api/auth/login",
                "/api/auth/register",
                "/api/auth/forgetpassword",
                "/api/auth/resetemail",
                "/swagger",
                "/health"
            };

            return skipPaths.Any(skipPath => path.StartsWith(skipPath));
        }

        private string? ExtractToken(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                return authHeader.Substring("Bearer ".Length).Trim();
            }

            return null;
        }

        private string? ExtractJtiFromToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jsonToken = tokenHandler.ReadJwtToken(token);
                return jsonToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;
            }
            catch
            {
                return null;
            }
        }
    }
}
