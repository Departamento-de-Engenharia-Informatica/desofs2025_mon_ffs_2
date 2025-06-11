using System.ComponentModel.DataAnnotations;

namespace AMAPP.API.DTOs.Auth
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [StringLength(254, ErrorMessage = "Email não pode exceder 254 caracteres")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(128, MinimumLength = 1, ErrorMessage = "Password deve ter entre 1 e 128 caracteres")]
        public string Password { get; set; }
    }
}
