using AMAPP.API.Extensions;
using FluentValidation;

namespace AMAPP.API.DTOs.Delivery.Validators;

public class CreateDeliveryDtoValidator : AbstractValidator<CreateDeliveryDto>
{
    public CreateDeliveryDtoValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0)
            .WithMessage("Order ID must be a valid positive number");

        RuleFor(x => x.DeliveryDate)
            .NotEmpty()
            .WithMessage("Delivery date is required")
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("Delivery date cannot be in the past");

        RuleFor(x => x.DeliveryLocation)
            .NotEmpty()
            .WithMessage("Delivery location is required")
            .Length(1, 200)
            .WithMessage("Delivery location must be between 1 and 200 characters")
            .SafeText();

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid delivery status");
    }
}
