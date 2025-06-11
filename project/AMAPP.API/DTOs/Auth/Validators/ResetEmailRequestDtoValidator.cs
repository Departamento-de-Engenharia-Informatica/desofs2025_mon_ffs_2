using AMAPP.API.Extensions;
using FluentValidation;


namespace AMAPP.API.DTOs.Auth.Validators
{
    public class ResetEmailRequestDtoValidator : AbstractValidator<ResetEmailRequestDto>
    {
        public ResetEmailRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email format")
                .MaximumLength(254)
                .WithMessage("Email cannot exceed 254 characters")
                .NoUnsafeChars();

            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage("Token is required")
                .MaximumLength(1000)
                .WithMessage("Token too long")
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
