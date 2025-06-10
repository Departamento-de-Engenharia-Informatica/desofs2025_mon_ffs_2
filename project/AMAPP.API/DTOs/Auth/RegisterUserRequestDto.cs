using System.ComponentModel.DataAnnotations;
using static AMAPP.API.Constants;

namespace AMAPP.API.DTOs.Auth
{
    public class RegisterUserRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public List<UserRole> Roles { get; set; }

    }
}
