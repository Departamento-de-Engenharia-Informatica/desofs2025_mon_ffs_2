namespace AMAPP.API.Services.Interfaces
{
    public interface IUserRoleInfoService
    {
        Task CreateRoleInfoAsync(string userId, List<string> roleNames);
        Task<bool> HasProducerInfoAsync(string userId);
        Task<bool> HasCoproducerInfoAsync(string userId);
    }
}
