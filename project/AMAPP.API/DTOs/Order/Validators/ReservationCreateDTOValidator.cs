using AMAPP.API.Extensions;
using FluentValidation;

namespace AMAPP.API.DTOs.Order.Validators
{
    public class ReservationCreateDTOValidator : AbstractValidator<ReservationCreateDTO>
    {
        public ReservationCreateDTOValidator()
        {
            RuleFor(x => x.Method)
                .IsInEnum()
                .WithMessage("Invalid delivery method");

            RuleFor(x => x.ReservationDate)
                .NotEmpty()
                .WithMessage("Reservation date is required")
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Reservation date cannot be in the past");

            RuleFor(x => x.Location)
                .NotEmpty()
                .WithMessage("Location is required")
                .Length(1, 200)
                .WithMessage("Location must be between 1 and 200 characters")
                .NoUnsafeChars();

            RuleFor(x => x.Notes)
                .MaximumLength(500)
                .WithMessage("Notes cannot exceed 500 characters")
                .NoUnsafeChars();
        }
    }
}
