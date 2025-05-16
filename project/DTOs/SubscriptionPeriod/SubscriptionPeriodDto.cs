using System;
using System.Collections.Generic;
using AMAPP.API.DTOs.DeliveryDate;

namespace AMAPP.API.DTOs.SubscriptionPeriod
{
    public class SubscriptionPeriodDto
    {
        public string? Name { get; set; }
        public SubscriptionDuration? Duration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DeliveryDateDto>? Dates { get; set; }
    }
}