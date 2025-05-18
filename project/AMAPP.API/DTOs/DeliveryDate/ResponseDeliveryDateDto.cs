namespace AMAPP.API.DTOs.DeliveryDate;

public class ResponseDeliveryDateDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int SubscriptionPeriodId { get; set; }
    public ResourceStatus ResourceStatus { get; set; }
}