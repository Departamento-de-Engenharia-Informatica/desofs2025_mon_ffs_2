using AMAPP.API.DTOs;

namespace AMAPP.API.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(MessageDto message);
    }
}
