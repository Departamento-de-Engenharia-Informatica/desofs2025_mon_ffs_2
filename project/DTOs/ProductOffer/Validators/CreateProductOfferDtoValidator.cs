using FluentValidation;

namespace AMAPP.API.DTOs.ProductOffer.Validators
{
    public class CreateProductOfferDtoValidator: AbstractValidator<CreateProductOfferDto>
    {
        public CreateProductOfferDtoValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("ProductId deve ser maior que zero.");
            RuleFor(x => x.SubscriptionPeriodId).GreaterThan(0).WithMessage("PeriodSubscriptionId deve ser maior que zero.");
            RuleFor(x => x.SelectedDeliveryDates).NotEmpty().WithMessage("As datas de entrega não pode ser vazio.");
            RuleFor(x => x.PaymentMethod).NotEmpty().WithMessage("Metodo de pagamento não pode ser vazio.");
            RuleFor(x => x.PaymentMode).NotEmpty().WithMessage("Modo de pagamento não pode ser vazio.");
        }
    }
}
