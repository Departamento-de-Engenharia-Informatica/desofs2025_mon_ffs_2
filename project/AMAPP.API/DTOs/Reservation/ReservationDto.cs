namespace AMAPP.API.DTOs.Reservation;
using static AMAPP.API.Constants;

public class ReservationDto
{ 
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DeliveryMethod Method { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
}