using System.ComponentModel.DataAnnotations;

namespace AMAPP.API.DTOs.Auth
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Password atual é obrigatória")]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nova password é obrigatória")]
        [StringLength(128, MinimumLength = 12, ErrorMessage = "Password deve ter entre 12 e 128 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{12,}$",
            ErrorMessage = "Password deve conter pelo menos: 1 minúscula, 1 maiúscula, 1 número e 1 carácter especial")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirmação da nova password é obrigatória")]
        [Compare("NewPassword", ErrorMessage = "Passwords não coincidem")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
