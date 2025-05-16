namespace AMAPP.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task Login(string username, string password);
    }
}
