using static AMAPP.API.Constants;

namespace AMAPP.API.DTOs.Order
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public DateTime PickupDate { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
    }
}
