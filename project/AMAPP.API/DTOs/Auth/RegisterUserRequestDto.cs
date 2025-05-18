using System.ComponentModel.DataAnnotations;

namespace AMAPP.API.DTOs.Auth
{
    public class RegisterUserRequestDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string ConfirmPassword { get; set; }

    }
}
