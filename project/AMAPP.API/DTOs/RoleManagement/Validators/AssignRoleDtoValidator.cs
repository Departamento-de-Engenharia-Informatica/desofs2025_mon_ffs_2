using FluentValidation;
using static AMAPP.API.Constants;

namespace AMAPP.API.DTOs.RoleManagement.Validators
{
    public class AssignRoleDtoValidator : AbstractValidator<AssignRoleDto>
    {
        public AssignRoleDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Username is required")
                .NotNull()
                .WithMessage("Username cannot be null")
                .EmailAddress()
                .WithMessage("Username must be a valid email address")
                .Length(1, 254) // Standard email max length
                .WithMessage("Username must be between 1 and 254 characters");

            RuleFor(x => x.RoleNames)
                .NotNull()
                .WithMessage("Role names list cannot be null")
                .NotEmpty()
                .WithMessage("At least one role must be specified")
                .Must(HaveAtLeastOneRole)
                .WithMessage("At least one role must be specified")
                .Must(ContainOnlyValidRoles)
                .WithMessage("Invalid role names detected. Valid roles are: Producer, CoProducer, Administrator, Amap")
                .Must(ContainNoDuplicateRoles)
                .WithMessage("Duplicate roles are not allowed");

            RuleForEach(x => x.RoleNames)
                .NotEmpty()
                .WithMessage("Role name cannot be empty")
                .Length(1, 256) // ASP.NET Identity Role name max length
                .WithMessage("Role name must be between 1 and 256 characters");
        }

        private bool HaveAtLeastOneRole(List<string> roleNames)
        {
            return roleNames != null && roleNames.Count > 0;
        }

        private bool ContainOnlyValidRoles(List<string> roleNames)
        {
            if (roleNames == null || !roleNames.Any())
                return true; // Handled by other rules

            var validRoles = new[]
            {
                RoleNames.Producer,
                RoleNames.CoProducer,
                RoleNames.Administrator,
                RoleNames.Amap
            };

            return roleNames.All(role => validRoles.Contains(role));
        }

        private bool ContainNoDuplicateRoles(List<string> roleNames)
        {
            if (roleNames == null)
                return true;

            return roleNames.Count == roleNames.Distinct().Count();
        }
    }
}
