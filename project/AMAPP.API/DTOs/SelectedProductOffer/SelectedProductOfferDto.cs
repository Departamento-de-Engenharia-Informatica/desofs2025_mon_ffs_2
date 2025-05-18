using AMAPP.API.DTOs.SubscriptionPayment;

namespace AMAPP.API.DTOs.SelectedProductOffer;

public class SelectedProductOfferDto
{ 
        public DateTime DeliveryDate { get; set; } = new();
        public int ProductOfferId { get; set; }
        public int SubscriptionId { get; set; }
        public int? Quantity { get; set; }
        public List<SubscriptionPaymentDto> SubscriptionPaymentDto  { get; set; } = new ();
}