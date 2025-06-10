using AMAPP.API.Extensions;
using FluentValidation;

namespace AMAPP.API.DTOs.Auth.Validators
{
    public class TwoFactorRequestDtoValidator : AbstractValidator<TwoFactorRequestDto>
    {
        public TwoFactorRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email format")
                .MaximumLength(254)
                .WithMessage("Email cannot exceed 254 characters")
                .NoUnsafeChars();

            RuleFor(x => x.Provider)
                .NotEmpty()
                .WithMessage("Provider is required")
                .Length(1, 50)
                .WithMessage("Provider must be between 1 and 50 characters")
                .NoUnsafeChars();

            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage("Token is required")
                .Length(1, 10)
                .WithMessage("Token must be between 1 and 10 characters")
                .Matches(@"^[0-9]+$")
                .WithMessage("Token must contain only numbers")
                .NoUnsafeChars();
        }
    }
}
