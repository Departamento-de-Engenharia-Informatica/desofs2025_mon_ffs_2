using static AMAPP.API.Constants;

namespace AMAPP.API.DTOs.Order
{
    public class ReservationCreateDTO
    {
        public DeliveryMethod Method { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
    }
}
