namespace AMAPP.API.DTOs.Order
{
    public class OrderDetailDTO : OrderDTO
    {
        public string DeliveryRequirements { get; set; }
        public ICollection<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
        public ReservationDTO Reservation { get; set; }
    }

}
