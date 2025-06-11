using AMAPP.API.Extensions;
using FluentValidation;

namespace AMAPP.API.DTOs.Order.Validators
{
    public class CreateOrderDTOValidator : AbstractValidator<CreateOrderDTO>
    {
        public CreateOrderDTOValidator()
        {
            RuleFor(x => x.DeliveryRequirements)
                .MaximumLength(500)
                .WithMessage("Delivery requirements cannot exceed 500 characters")
                .SafeText();

            RuleFor(x => x.OrderItems)
                .NotEmpty()
                .WithMessage("Order must have at least one item")
                .Must(items => items.Count <= 50)
                .WithMessage("Order cannot have more than 50 items");

            RuleForEach(x => x.OrderItems)
                .SetValidator(new CreateOrderItemDTOValidator());
        }
    }
}
