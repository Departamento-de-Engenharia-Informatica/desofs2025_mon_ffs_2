using static AMAPP.API.Constants;

namespace AMAPP.API.DTOs.Order
{
    public class OrderStatusUpdateDTO
    {
        public int ProducerId { get; set; }
        public Constants.OrderStatus Status { get; set; }
        public List<int> OrderItemIds { get; set; } // Added property to fix CS1061  
        public Constants.OrderItemStatus? ItemStatus { get; set; } // Added property for item-specific status  
    }
}
