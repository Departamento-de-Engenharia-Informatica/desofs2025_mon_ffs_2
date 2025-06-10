using AMAPP.API.Extensions;
using FluentValidation;

namespace AMAPP.API.DTOs.Auth.Validators
{
    public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email format")
                .MaximumLength(254)
                .WithMessage("Email cannot exceed 254 characters")
                .NoUnsafeChars();

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .Length(1, 128)
                .WithMessage("Password must be between 1 and 128 characters")
                .NoUnsafeChars();
        }
    }
}
