using System.ComponentModel.DataAnnotations;

namespace AMAPP.API.DTOs.Auth
{
    public class RegisterUserRequestDto
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s\-']+$", ErrorMessage = "Primeiro nome contém caracteres inválidos")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s\-']+$", ErrorMessage = "Último nome contém caracteres inválidos")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(254, ErrorMessage = "Email não pode exceder 254 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(128, MinimumLength = 12, ErrorMessage = "Password must be between 12 and 128 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{12,}$",
            ErrorMessage = "Password deve conter pelo menos: 1 minúscula, 1 maiúscula, 1 número e 1 carácter especial")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }

    }
}
