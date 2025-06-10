using AMAPP.API.Extensions;
using FluentValidation;

namespace AMAPP.API.DTOs.Order.Validators
{
    public class UpdateOrderDTOValidator : AbstractValidator<UpdateOrderDTO>
    {
        public UpdateOrderDTOValidator()
        {
            RuleFor(x => x.DeliveryRequirements)
                .MaximumLength(500)
                .WithMessage("Delivery requirements cannot exceed 500 characters")
                .NoUnsafeChars();

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Invalid order status");
        }
    }
}
