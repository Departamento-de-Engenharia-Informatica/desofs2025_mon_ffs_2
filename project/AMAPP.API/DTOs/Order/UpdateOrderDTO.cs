using static AMAPP.API.Constants;

namespace AMAPP.API.DTOs.Order
{
    public class UpdateOrderDTO
    {
        public string DeliveryRequirements { get; set; }
        public OrderStatus Status { get; set; }
    }
}
