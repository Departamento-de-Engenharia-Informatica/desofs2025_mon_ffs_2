using System.ComponentModel.DataAnnotations;
using static AMAPP.API.Constants;

namespace AMAPP.API.DTOs.Delivery
{
    public class CreateDeliveryDto
    {
        public int OrderId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryLocation { get; set; }
        public DeliveryStatus Status { get; set; }
    }
}
