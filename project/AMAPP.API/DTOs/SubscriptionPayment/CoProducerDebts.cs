namespace AMAPP.API.DTOs.SubscriptionPayment
{
    public class CoProducerDebts
    {
        public string CoProducerName { get; set; }
        public string ProducerName { get; set; }
        public string SubscriptionName { get; set; }
        public decimal Debt { get; set; }
    }
}
