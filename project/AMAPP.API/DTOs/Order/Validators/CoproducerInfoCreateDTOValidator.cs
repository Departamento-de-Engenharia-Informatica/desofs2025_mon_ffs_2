using AMAPP.API.Extensions;
using FluentValidation;

namespace AMAPP.API.DTOs.Order.Validators;

public class CoproducerInfoCreateDTOValidator : AbstractValidator<CoproducerInfoCreateDTO>
{
    public CoproducerInfoCreateDTOValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required")
            .Length(1, 450) // GUID max length in ASP.NET Identity
            .WithMessage("User ID must be between 1 and 450 characters")
            .NoUnsafeChars();
    }
}
