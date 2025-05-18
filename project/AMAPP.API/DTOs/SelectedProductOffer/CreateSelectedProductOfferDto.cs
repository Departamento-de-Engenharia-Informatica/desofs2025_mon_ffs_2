using System.ComponentModel.DataAnnotations;
using AMAPP.API.DTOs.SubscriptionPayment;

namespace AMAPP.API.DTOs.SelectedProductOffer;

public class CreateSelectedProductOfferDto
{
        [Required] 
        public DateTime DeliveryDate { get; set; } = new();
        [Required]
        public int ProductOfferId { get; set; }
        [Required]
        public int SubscriptionId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public List<CreateSubscriptionPaymentDto> CreateSubscriptionPaymentsDto  { get; set; } = new ();
}