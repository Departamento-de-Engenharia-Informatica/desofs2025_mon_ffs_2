using System.ComponentModel.DataAnnotations;
using AMAPP.API.DTOs.SubscriptionPayment;

namespace AMAPP.API.DTOs.SelectedProductOffer;

public class ResponseSelectedProductOfferDto
{
        public int Id { get; set; }
        public DateTime DeliveryDate { get; set; } = new();
        public int ProductOfferId { get; set; }
        public int SubscriptionId { get; set; }
        public int Quantity { get; set; }
        public int SubscriptionPeriodId  { get; set; }
        public List<ResponseSubscriptionPaymentDto> ResponseSubscriptionPaymentDto  { get; set; } = new ();
}