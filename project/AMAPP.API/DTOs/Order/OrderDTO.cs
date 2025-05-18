using static AMAPP.API.Constants;

namespace AMAPP.API.DTOs.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int CoproducerInfoId { get; set; }
        public string CoproducerName { get; set; }   
        public DateTime OrderDate { get; set; }
        public string DeliveryRequirements { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
