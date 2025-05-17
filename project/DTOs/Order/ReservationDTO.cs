using static AMAPP.API.Constants;

namespace AMAPP.API.DTOs.Order
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public DeliveryMethod Method { get; set; }
        public string MethodName => Method.ToString();
        public DateTime ReservationDate { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
    }
}
