using AMAPP.API.DTOs.Order;

public class CreateOrderDTO
{
    //public int CoproducerInfoId { get; set; }
    public string DeliveryRequirements { get; set; }
    public List<CreateOrderItemDTO> OrderItems { get; set; } = new List<CreateOrderItemDTO>();

}