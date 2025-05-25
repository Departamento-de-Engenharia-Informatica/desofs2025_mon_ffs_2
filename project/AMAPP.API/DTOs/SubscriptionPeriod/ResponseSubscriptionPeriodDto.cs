using System.ComponentModel.DataAnnotations;
using AMAPP.API.DTOs.DeliveryDate;

namespace AMAPP.API.DTOs.SubscriptionPeriod
{
    public class ResponseSubscriptionPeriodDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SubscriptionDuration Duration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public List<ResponseDeliveryDateDto> Dates { get; set; } = new();
        
        public ResourceStatus ResourceStatus { get; set; }
    }
}