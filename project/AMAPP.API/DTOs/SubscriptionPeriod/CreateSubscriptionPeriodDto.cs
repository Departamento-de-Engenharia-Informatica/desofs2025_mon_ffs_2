using System.ComponentModel.DataAnnotations;
using AMAPP.API.DTOs.DeliveryDate;

namespace AMAPP.API.DTOs.SubscriptionPeriod
{
    public class CreateSubscriptionPeriodDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public SubscriptionDuration Duration { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        
        [Required]
        public List<CreateDeliveryDateDto> Dates { get; set; } = new ();
    }
}