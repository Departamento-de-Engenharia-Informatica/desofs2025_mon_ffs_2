using static AMAPP.API.Constants;

namespace AMAPP.API.DTOs.ProductOffer
{
    public class ProductOfferDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SubscriptionPeriodId { get; set; }
        public List<int> SelectedDeliveryDates { get; set; } = new List<int>(); // List of Delivery Date IDs, or other details
        public List<int> ProductOfferPaymentMethodIds { get; set; } = new List<int>(); // List of Payment Method IDs
        public List<int> ProductOfferPaymentModeIds { get; set; } = new List<int>(); // List of Payment Mode IDs
        public string ProductName { get; set; } // Nome do produto

    } 

}
