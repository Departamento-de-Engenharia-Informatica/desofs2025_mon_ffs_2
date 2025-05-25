using FluentValidation;
using System;
using AMAPP.API.DTOs.SubscriptionPayment.Validators;

namespace AMAPP.API.DTOs.SelectedProductOffer.Validators
{
    public class CreateSelectedProductOfferDtoValidator : AbstractValidator<CreateSelectedProductOfferDto>
    {
        public CreateSelectedProductOfferDtoValidator()
        {
            RuleFor(x => x.DeliveryDate)
                .NotEmpty().WithMessage("A data de entrega é obrigatória.")
                .Must(date => date != default(DateTime)).WithMessage("A data de entrega não deve ser um valor padrão.");

            RuleFor(x => x.ProductOfferId)
                .GreaterThan(0).WithMessage("O identificador da oferta de produto deve ser maior que 0.");

            RuleFor(x => x.SubscriptionId)
                .GreaterThan(0).WithMessage("O identificador da assinatura deve ser maior que 0.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("A quantidade deve ser maior que 0.");
            
            RuleForEach(x => x.CreateSubscriptionPaymentsDto)
                .SetValidator(new CreateSubscriptionPaymentDtoValidator());
        }
    }
}