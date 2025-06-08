﻿using AMAPP.API.Utils;
using FluentValidation;

namespace AMAPP.API.DTOs.Product.Validators;

public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.") // Ensure it's not null or empty
            .Length(3, 100).WithMessage("Name must be between 3 and 100 characters.") // Minimum and maximum length
            .Matches(@"^[a-zA-Z0-9\s]+$").WithMessage("Name can only contain alphanumeric characters and spaces."); // Optional: restrict to alphanumeric + spaces

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(255).WithMessage("Description must not exceed 255 characters."); // Limit the description length

        RuleFor(x => x.DeliveryUnit)
            .NotEmpty().WithMessage("Delivery unit is required.")
            .IsInEnum().WithMessage("Invalid delivery unit.");

        RuleFor(x => x.ReferencePrice)
           .GreaterThan(0).WithMessage("Reference price must be greater than 0.");

        RuleFor(x => x.ProductTypeId)
            .GreaterThan(0).WithMessage("Invalid product type Id.");

        RuleFor(x => x.Photo)
               .Must(ImageSecurityHelper.IsValidImage)
               .WithMessage(x => ImageSecurityHelper.GetImageValidationError(x.Photo))
               .When(x => x.Photo != null);

    }
}
