namespace AMAPP.API.DTOs.SubscriptionPayment;

public class UpdateSubscriptionPaymentDto
{
    public DateTime PaymentDate { get; set; }
    public double Amount { get; set; }
    public Constants.PaymentStatus PaymentStatus { get; set; }
}