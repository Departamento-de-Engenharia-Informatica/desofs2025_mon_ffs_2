using static AMAPP.API.Constants;

namespace AMAPP.API.DTOs.Delivery
{
    public class UpdateDeliveryDto
    {
        public DateTime DeliveryDate { get; set; }
        public string DeliveryLocation { get; set; }
        public DeliveryStatus Status { get; set; }
    }
}
