namespace AMAPP.API.DTOs.SubscriptionPayment
{
    public class ProducerPendingPayments
    {
        public string CoProducerName { get; set; }
        public string ProducerName { get; set; }
        public string SubscriptionName { get; set; }
        public decimal PendingPayment { get; set; }
    }
}
