using FluentValidation;
using AMAPP.API.DTOs.ProductOffer;

namespace AMAPP.API.DTOs.ProductOffer.Validators
{
    public class ProductOfferDtoValidator : AbstractValidator<ProductOfferDto>
    {
        public ProductOfferDtoValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("ProductId deve ser maior que zero.");
            RuleFor(x => x.SubscriptionPeriodId).GreaterThan(0).WithMessage("PeriodSubscriptionId deve ser maior que zero.");
            RuleFor(x => x.SelectedDeliveryDates).NotEmpty().WithMessage("SelectedDeliveryDates não pode ser vazio.");
            //RuleForEach(x => x.SelectedDeliveryDates).Must(date => date > DateTime.MinValue).WithMessage("Data de entrega inválida.");
        }
    }
}
