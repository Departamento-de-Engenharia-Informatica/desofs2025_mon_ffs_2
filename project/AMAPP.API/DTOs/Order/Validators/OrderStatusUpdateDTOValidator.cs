using FluentValidation;

namespace AMAPP.API.DTOs.Order.Validators
{
    public class OrderStatusUpdateDTOValidator : AbstractValidator<OrderStatusUpdateDTO>
    {
        public OrderStatusUpdateDTOValidator()
        {
            RuleFor(x => x.ProducerId)
                .GreaterThan(0)
                .WithMessage("Producer ID must be a valid positive number");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Invalid order status");

            RuleFor(x => x.OrderItemIds)
                .NotEmpty()
                .WithMessage("Order item IDs are required")
                .Must(ids => ids.Count <= 50)
                .WithMessage("Cannot update more than 50 items at once");

            RuleForEach(x => x.OrderItemIds)
                .GreaterThan(0)
                .WithMessage("Order item ID must be a valid positive number");

            RuleFor(x => x.ItemStatus)
                .IsInEnum()
                .WithMessage("Invalid item status")
                .When(x => x.ItemStatus.HasValue);
        }
    }
}
