using AMAPP.API.DTOs.Product;
using FluentValidation;
using System.Xml.Linq;
using AMAPP.API.Utils;
using AMAPP.API.Extensions;

namespace AMAPP.API.DTOs.Product.Validators;

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .Length(2, 100)
            .WithMessage("Name must be between 2 and 100 characters")
            .SafeName()
            .NoUnsafeChars();

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters")
            .NoUnsafeChars();

        RuleFor(x => x.DeliveryUnit)
            .IsInEnum()
            .WithMessage("Invalid delivery unit");

        RuleFor(x => x.ReferencePrice)
            .GreaterThan(0)
            .WithMessage("Reference price must be greater than 0")
            .LessThan(100000)
            .WithMessage("Reference price too high");

        RuleFor(x => x.ProductTypeId)
            .GreaterThan(0)
            .WithMessage("Invalid product type ID");

        RuleFor(x => x.Photo)
            .Must(ImageSecurityHelper.IsValidImage)
            .WithMessage(x => ImageSecurityHelper.GetImageValidationError(x.Photo))
            .When(x => x.Photo != null);

    }
}
