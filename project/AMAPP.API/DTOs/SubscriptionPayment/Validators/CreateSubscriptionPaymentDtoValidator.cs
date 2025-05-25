using FluentValidation;

namespace AMAPP.API.DTOs.SubscriptionPayment.Validators
{
    public class CreateSubscriptionPaymentDtoValidator : AbstractValidator<CreateSubscriptionPaymentDto>
    {
        public CreateSubscriptionPaymentDtoValidator()
        {
            RuleFor(x => x.PaymentDate).NotEmpty().WithMessage("PaymentDate is required.");
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount is required.");
            // Only validate SelectedProductOfferId if it's present
            When(x => x.SelectedProductOfferId != 0, () =>
            {
                RuleFor(x => x.SelectedProductOfferId).GreaterThan(0).WithMessage("SelectedProductOfferId must be greater than 0.");
            });
        }
    }
}