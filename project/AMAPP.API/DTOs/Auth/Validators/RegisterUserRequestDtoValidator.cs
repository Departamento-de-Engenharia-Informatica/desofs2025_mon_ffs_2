using AMAPP.API.Extensions;
using FluentValidation;

namespace AMAPP.API.DTOs.Auth.Validators
{
    public class RegisterUserRequestDtoValidator : AbstractValidator<RegisterUserRequestDto>
    {
        public RegisterUserRequestDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name is required")
                .Length(1, 50)
                .WithMessage("First name must be between 1 and 50 characters")
                .SafeName()
                .SafeText();

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last name is required")
                .Length(1, 50)
                .WithMessage("Last name must be between 1 and 50 characters")
                .SafeName()
                .SafeText();

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
                .StrongPassword();

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Password confirmation is required")
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match");
        }
    }
}
