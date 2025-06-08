using System.Security.Claims;

namespace AMAPP.API.Extensions
{
    /// <summary>
    /// Extension methods para funcionalidades de segurança
    /// </summary>
    public static class SecurityExtensions
    {
        /// <summary>
        /// Obtém o endereço IP real do cliente, considerando proxies e load balancers
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>Endereço IP do cliente ou "Unknown" se não conseguir determinar</returns>
        public static string GetClientIpAddress(this HttpContext context)
        {
            // Tentar obter IP através de headers de proxy (ordem de prioridade)
            var ipAddress = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (!string.IsNullOrEmpty(ipAddress))
            {
                // X-Forwarded-For pode conter múltiplos IPs separados por vírgula
                // O primeiro é geralmente o IP original do cliente
                ipAddress = ipAddress.Split(',')[0].Trim();

                // Validar se é um IP válido
                if (IsValidIpAddress(ipAddress))
                {
                    return ipAddress;
                }
            }

            // Tentar X-Real-IP (usado por alguns proxies)
            ipAddress = context.Request.Headers["X-Real-IP"].FirstOrDefault();
            if (!string.IsNullOrEmpty(ipAddress) && IsValidIpAddress(ipAddress))
            {
                return ipAddress;
            }

            // Tentar CF-Connecting-IP (Cloudflare)
            ipAddress = context.Request.Headers["CF-Connecting-IP"].FirstOrDefault();
            if (!string.IsNullOrEmpty(ipAddress) && IsValidIpAddress(ipAddress))
            {
                return ipAddress;
            }

            // Fallback para IP da conexão direta
            ipAddress = context.Connection.RemoteIpAddress?.ToString();
            if (!string.IsNullOrEmpty(ipAddress))
            {
                // Converter IPv6 loopback para IPv4
                if (ipAddress == "::1")
                {
                    return "127.0.0.1";
                }

                // Remover prefixo IPv6 se presente
                if (ipAddress.StartsWith("::ffff:"))
                {
                    ipAddress = ipAddress.Substring(7);
                }

                return ipAddress;
            }

            return "Unknown";
        }

        /// <summary>
        /// Obtém o ID do utilizador atual dos claims
        /// </summary>
        /// <param name="user">ClaimsPrincipal</param>
        /// <returns>ID do utilizador ou "Anonymous" se não autenticado</returns>
        public static string GetCurrentUserId(this ClaimsPrincipal user)
        {
            if (user?.Identity?.IsAuthenticated != true)
                return "Anonymous";

            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? user.FindFirst("sub")?.Value
                ?? "Unknown";
        }

        /// <summary>
        /// Obtém o email do utilizador atual dos claims
        /// </summary>
        /// <param name="user">ClaimsPrincipal</param>
        /// <returns>Email do utilizador ou "Unknown" se não disponível</returns>
        public static string GetCurrentUserEmail(this ClaimsPrincipal user)
        {
            if (user?.Identity?.IsAuthenticated != true)
                return "Anonymous";

            return user.FindFirst(ClaimTypes.Email)?.Value
                ?? user.FindFirst("email")?.Value
                ?? "Unknown";
        }

        /// <summary>
        /// Obtém o nome completo do utilizador atual dos claims
        /// </summary>
        /// <param name="user">ClaimsPrincipal</param>
        /// <returns>Nome completo ou "Unknown"</returns>
        public static string GetCurrentUserFullName(this ClaimsPrincipal user)
        {
            if (user?.Identity?.IsAuthenticated != true)
                return "Anonymous";

            var firstName = user.FindFirst("FirstName")?.Value ?? "";
            var lastName = user.FindFirst("LastName")?.Value ?? "";

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return $"{firstName} {lastName}".Trim();
            }

            return user.FindFirst(ClaimTypes.Name)?.Value
                ?? user.FindFirst("name")?.Value
                ?? "Unknown";
        }

        /// <summary>
        /// Obtém todas as roles do utilizador atual
        /// </summary>
        /// <param name="user">ClaimsPrincipal</param>
        /// <returns>Lista de roles</returns>
        public static List<string> GetCurrentUserRoles(this ClaimsPrincipal user)
        {
            if (user?.Identity?.IsAuthenticated != true)
                return new List<string>();

            return user.FindAll(ClaimTypes.Role)?.Select(c => c.Value).ToList()
                ?? new List<string>();
        }

        /// <summary>
        /// Verifica se o utilizador tem uma role específica
        /// </summary>
        /// <param name="user">ClaimsPrincipal</param>
        /// <param name="role">Nome da role</param>
        /// <returns>True se tem a role</returns>
        public static bool HasRole(this ClaimsPrincipal user, string role)
        {
            if (user?.Identity?.IsAuthenticated != true)
                return false;

            return user.IsInRole(role);
        }

        /// <summary>
        /// Verifica se o utilizador tem qualquer uma das roles especificadas
        /// </summary>
        /// <param name="user">ClaimsPrincipal</param>
        /// <param name="roles">Roles a verificar</param>
        /// <returns>True se tem pelo menos uma das roles</returns>
        public static bool HasAnyRole(this ClaimsPrincipal user, params string[] roles)
        {
            if (user?.Identity?.IsAuthenticated != true)
                return false;

            return roles.Any(role => user.IsInRole(role));
        }

        /// <summary>
        /// Obtém informações de contexto de segurança para logging
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>Objeto com informações de contexto</returns>
        //public static SecurityContext GetSecurityContext(this HttpContext context)
        //{
        //    var user = context.User;

        //    return new SecurityContext
        //    {
        //        UserId = user.GetCurrentUserId(),
        //        UserEmail = user.GetCurrentUserEmail(),
        //        UserName = user.GetCurrentUserFullName(),
        //        Roles = user.GetCurrentUserRoles(),
        //        ClientIp = context.GetClientIpAddress(),
        //        UserAgent = context.Request.Headers["User-Agent"].FirstOrDefault() ?? "Unknown",
        //        RequestPath = context.Request.Path.Value ?? "",
        //        RequestMethod = context.Request.Method,
        //        IsAuthenticated = user.Identity?.IsAuthenticated ?? false,
        //        Timestamp = DateTime.UtcNow
        //    };
        //}

        /// <summary>
        /// Obtém o User-Agent do request
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>User-Agent ou "Unknown"</returns>
        public static string GetUserAgent(this HttpContext context)
        {
            return context.Request.Headers["User-Agent"].FirstOrDefault() ?? "Unknown";
        }

        /// <summary>
        /// Verifica se é um request de um bot ou crawler
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>True se for um bot</returns>
        public static bool IsBot(this HttpContext context)
        {
            var userAgent = context.GetUserAgent().ToLowerInvariant();

            var botIndicators = new[]
            {
                "bot", "crawler", "spider", "scraper", "curl", "wget",
                "python-requests", "facebookexternalhit", "twitterbot"
            };

            return botIndicators.Any(indicator => userAgent.Contains(indicator));
        }

        #region Private Methods

        /// <summary>
        /// Valida se uma string é um endereço IP válido
        /// </summary>
        /// <param name="ipAddress">String a validar</param>
        /// <returns>True se for um IP válido</returns>
        private static bool IsValidIpAddress(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                return false;

            return System.Net.IPAddress.TryParse(ipAddress, out _);
        }

        #endregion
    }
}
