﻿using AMAPP.API.Extensions;
using FluentValidation;


using FluentValidation;

namespace AMAPP.API.DTOs.Delivery.Validators;

public class UpdateDeliveryDtoValidator : AbstractValidator<UpdateDeliveryDto>
{
    public UpdateDeliveryDtoValidator()
    {
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