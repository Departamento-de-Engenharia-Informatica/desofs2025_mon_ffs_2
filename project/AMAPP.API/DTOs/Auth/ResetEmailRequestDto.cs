using System.ComponentModel.DataAnnotations;

namespace AMAPP.API.DTOs.Auth
{
    public class ResetEmailRequestDto
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Token é obrigatório")]
        public string Token { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password é obrigatória")]
        [StringLength(128, MinimumLength = 12, ErrorMessage = "Password deve ter entre 12 e 128 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{12,}$",
            ErrorMessage = "Password deve conter pelo menos: 1 minúscula, 1 maiúscula, 1 número e 1 carácter especial")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirmação de password é obrigatória")]
        [Compare("Password", ErrorMessage = "Passwords não coincidem")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
