namespace AMAPP.API.DTOs.SubscriptionPayment;
using static AMAPP.API.Constants;

public class ResponseSubscriptionPaymentDto
{
    public int Id { get; set; }
    public int SubscriptionId { get; set; }

    public int SelectedProductOfferId { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public PaymentStatus PaymentStatus { get; set; }
}