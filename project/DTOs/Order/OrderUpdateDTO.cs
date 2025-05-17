namespace AMAPP.API.DTOs.Order
{
    public class OrderUpdateDTO
    {
        public int CoproducerInfoId { get; set; }
        public string DeliveryRequirements { get; set; }
        public ICollection<OrderItemUpdateDTO> OrderItems { get; set; } = new List<OrderItemUpdateDTO>();
        public ReservationUpdateDTO Reservation { get; set; }
    }
}
