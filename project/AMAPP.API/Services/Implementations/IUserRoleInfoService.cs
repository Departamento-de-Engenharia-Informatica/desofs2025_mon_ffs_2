namespace AMAPP.API.Services.Implementations
{
    public interface IUserRoleInfoService
    {
        Task CreateRoleInfoAsync(string userId, List<string> roleNames);
        Task<bool> HasProducerInfoAsync(string userId);
        Task<bool> HasCoproducerInfoAsync(string userId);
    }
}
