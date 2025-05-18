
namespace AMAPP.API.DTOs.ProductOffer
{
    public class ProductOfferDetailsDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SubscriptionPeriodId { get; set; }
        public List<DateTime> DeliveryDates { get; set; } = new List<DateTime>(); // List of Delivery Date IDs, or other details
        public string ProductName { get; set; } // Nome do produto
        public List<Constants.PaymentMethod> PaymentMethods { get; set; } 
        public List<Constants.PaymentMode> PaymentModes { get; set; } 
    }
}
