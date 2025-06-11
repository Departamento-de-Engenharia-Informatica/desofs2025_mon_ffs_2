using FluentValidation;

namespace AMAPP.API.DTOs.Order.Validators
{
    public class UpdateOrderItemDTOValidator : AbstractValidator<UpdateOrderItemDTO>
    {
        public UpdateOrderItemDTOValidator()
        {
            RuleFor(x => x.OrderItemId)
                .GreaterThan(0)
                .WithMessage("Order item ID must be a valid positive number");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0")
                .LessThanOrEqualTo(1000)
                .WithMessage("Quantity cannot exceed 1000");
        }
    }
}
