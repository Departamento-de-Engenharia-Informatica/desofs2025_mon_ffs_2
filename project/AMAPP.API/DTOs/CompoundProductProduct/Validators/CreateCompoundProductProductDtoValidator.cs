using AMAPP.API.DTOs.Product;
using AMAPP.API.DTOs.Product.Validators;
using FluentValidation;

namespace AMAPP.API.DTOs.CompoundProductProduct.Validators;

public class CreateCompoundProductProductDtoValidator : AbstractValidator<CreateCompoundProductProductDto>
{
    public CreateCompoundProductProductDtoValidator()
    {
        RuleFor(x => x.CompoundProduct)
            .SetValidator(new CreateProductDtoValidator());

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId list is required.")
            .Must(ids => ids != null && ids > 0).WithMessage("All ProductIds must be greater than 0.");
    }
}