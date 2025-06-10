namespace AMAPP.API.Services.Interfaces
{
    public interface ITokenBlacklistService
    {
        Task RevokeTokenAsync(string tokenId, DateTime expiration);
        Task<bool> IsTokenRevokedAsync(string token);
        Task CleanupExpiredTokensAsync(string token);
    }
}
