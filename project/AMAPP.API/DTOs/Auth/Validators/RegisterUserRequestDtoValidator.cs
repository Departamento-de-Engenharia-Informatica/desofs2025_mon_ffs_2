using AMAPP.API.Extensions;
using FluentValidation;
using static AMAPP.API.Constants;

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

            RuleFor(x => x.Roles)
                .NotNull()
                .WithMessage("Roles list cannot be null")
                .NotEmpty()
                .WithMessage("At least one role must be selected")
                .Must(ContainOnlyValidRegistrationRoles)
                .WithMessage("Only Producer and CoProducer roles are allowed for registration")
                .Must(ContainNoDuplicates)
                .WithMessage("Duplicate roles are not allowed");
        }

        private bool ContainOnlyValidRegistrationRoles(List<UserRole> roles)
        {
            if (roles == null || !roles.Any())
                return false;

            // Apenas Producer e CoProducer podem ser registrados via endpoint público
            var allowedRoles = new[] { UserRole.Producer, UserRole.CoProducer };
            return roles.All(role => allowedRoles.Contains(role));
        }

        private bool ContainNoDuplicates(List<UserRole> roles)
        {
            if (roles == null)
                return true;

            return roles.Count == roles.Distinct().Count();
        }
    }
}
