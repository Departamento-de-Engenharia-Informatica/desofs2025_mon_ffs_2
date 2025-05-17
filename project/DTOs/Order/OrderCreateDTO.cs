namespace AMAPP.API.DTOs.Order
{
    public class OrderCreateDTO
    {
        public int CoproducerInfoId { get; set; }
        public string DeliveryRequirements { get; set; }
        public ICollection<OrderItemCreateDTO> OrderItems { get; set; } = new List<OrderItemCreateDTO>();
        public ReservationCreateDTO Reservation { get; set; }
    }
}
