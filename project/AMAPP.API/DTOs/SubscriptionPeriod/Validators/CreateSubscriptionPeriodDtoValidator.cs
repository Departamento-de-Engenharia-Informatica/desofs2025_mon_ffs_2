using FluentValidation;
using System;
using System.Linq;

namespace AMAPP.API.DTOs.SubscriptionPeriod.Validators
{
    public class CreateSubscriptionPeriodDtoValidator : AbstractValidator<CreateSubscriptionPeriodDto>
    {
        public CreateSubscriptionPeriodDtoValidator()
        {
            
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(2, 50).WithMessage("O nome deve ter entre 2 e 50 caracteres.");

            /*RuleFor(x => x.StartDate)
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("A data de início deve ser hoje ou no futuro."); //deprecated
            */

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("A data de término do periodo de subscrição deve ser igual ou superior à data de início.");
             
            RuleForEach(x => x.Dates)
                //.SetValidator(new CreateDeliveryDateDtoValidator())
                .Must((dto, date) => date.Date >= dto.StartDate && date.Date <= dto.EndDate)
                .WithMessage("Cada data de entrega deve estar dentro do período de subscrição.");
            
            //validate if the interval is within the duration
            RuleFor(x => x)
                .Must(dto => IsDurationMatchingInterval(dto.StartDate, dto.EndDate, dto.Duration))
                .WithMessage("Periocidade de subscrição inválida.");
        }
        
        private bool IsDurationMatchingInterval(DateTime startDate, DateTime endDate, SubscriptionDuration duration)
        {
            int actualDays = (endDate - startDate).Days;
            int durationDays = duration.GetDurationDays();

            // Check if the actual days fit within the selected duration
            if (actualDays > durationDays)
            {
                return false;
            }

            // Check if the actual days fit within a shorter duration
            foreach (var kvp in SubscriptionDurationExtensions.DurationDays)
            {
                if (kvp.Value >= actualDays && kvp.Value < durationDays)
                {
                    return false;
                }
            }

            return true;
        }
    }
}