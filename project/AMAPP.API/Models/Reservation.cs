using static AMAPP.API.Constants;

namespace AMAPP.API.Models

{
    // Reservation model for order delivery or pickup
    public class Reservation
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public DeliveryMethod Method { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
    }
}
