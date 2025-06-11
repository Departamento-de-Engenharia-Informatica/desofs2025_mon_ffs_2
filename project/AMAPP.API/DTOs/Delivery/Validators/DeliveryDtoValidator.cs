using AMAPP.API.Extensions;
using FluentValidation;

namespace AMAPP.API.DTOs.Delivery.Validators;

public class DeliveryDtoValidator : AbstractValidator<DeliveryDto>
{
    public DeliveryDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Delivery ID must be a valid positive number");

        RuleFor(x => x.OrderId)
            .GreaterThan(0)
            .WithMessage("Order ID must be a valid positive number");

        RuleFor(x => x.DeliveryDate)
            .NotEmpty()
            .WithMessage("Delivery date is required");

        RuleFor(x => x.DeliveryLocation)
            .NotEmpty()
            .WithMessage("Delivery location is required")
            .Length(1, 200)
            .WithMessage("Delivery location must be between 1 and 200 characters")
            .NoUnsafeChars();

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid delivery status");
    }
}
