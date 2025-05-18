namespace AMAPP.API.DTOs.DeliveryDate;

public class DeliveryDateDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public ResourceStatus ResourceStatus { get; set; } = ResourceStatus.Ativo; //default value
}