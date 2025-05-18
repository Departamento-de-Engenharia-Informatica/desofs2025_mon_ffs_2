using System.ComponentModel.DataAnnotations;

namespace AMAPP.API.DTOs.SubscriptionPayment;

public class CreateSubscriptionPaymentDto
{
    
    public int SelectedProductOfferId { get; set; }
    [Required]
    public DateTime PaymentDate { get; set; }
    [Required]
    public decimal Amount { get; set; }
}