using System.ComponentModel.DataAnnotations;

namespace AMAPP.API.DTOs.Auth
{
    public class TwoFactorRequestDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [StringLength(254, ErrorMessage = "Email não pode exceder 254 caracteres")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Provider is required")]
        public string? Provider { get; set; }
        [Required(ErrorMessage = "Token is required")]
        public string? Token { get; set; }
    }
}
