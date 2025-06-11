using AMAPP.API.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace AMAPP.API.Services.Implementations
{
    public class MemoryCacheTokenBlacklistService: ITokenBlacklistService
    {
        // This class is a placeholder for the database token blacklist service implementation.
        // It should implement methods to add, check, and remove tokens from the blacklist.
        // For now, it is empty as per the request.
        // Example methods could include:
        // - AddTokenToBlacklist(string token)
        // - IsTokenBlacklisted(string token)
        // - RemoveTokenFromBlacklist(string token)
        // These methods would interact with a database context to manage the blacklist.

        private readonly IMemoryCache _cache;
        private readonly ILogger<MemoryCacheTokenBlacklistService> _logger;
        private const string BLACKLIST_PREFIX = "revoked_token_";

        public MemoryCacheTokenBlacklistService(IMemoryCache cache, ILogger<MemoryCacheTokenBlacklistService> logger)
        {
            _cache = cache;
            _logger = logger;
        }



        // Example method to add a token to the blacklist
        public async Task RevokeTokenAsync(string tokenId, DateTime expiration)
        {
            var key = $"{BLACKLIST_PREFIX}{tokenId}";

            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = expiration,
                Priority = CacheItemPriority.High
            };

            _cache.Set(key, true, options);
            _logger.LogInformation("Token {TokenId} added to blacklist (MemoryCache)", tokenId[..8]);

            await Task.CompletedTask;
        }

        public async Task<bool> IsTokenRevokedAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return true;

            var key = $"{BLACKLIST_PREFIX}{token}";
            var isRevoked = _cache.TryGetValue(key, out _);

            return await Task.FromResult(isRevoked);
        }

        public async Task CleanupExpiredTokensAsync(string token)
        {
            // MemoryCache limpa automaticamente
            await Task.CompletedTask;
        }
    }
}
