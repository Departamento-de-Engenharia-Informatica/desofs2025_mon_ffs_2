using AMAPP.API.Extensions;
using FluentValidation;

namespace AMAPP.API.DTOs.Auth.Validators;

public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordDtoValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .WithMessage("Current password is required")
            .MaximumLength(128)
            .WithMessage("Current password too long")
            .NoUnsafeChars();

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage("New password is required")
            .StrongPassword();

        RuleFor(x => x.ConfirmNewPassword)
            .NotEmpty()
            .WithMessage("Password confirmation is required")
            .Equal(x => x.NewPassword)
            .WithMessage("Passwords do not match");
    }
}
