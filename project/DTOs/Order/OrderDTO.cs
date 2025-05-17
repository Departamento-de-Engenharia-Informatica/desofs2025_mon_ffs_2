using static AMAPP.API.Constants;

namespace AMAPP.API.DTOs.Order
{
    // Base Order DTO with common properties
    public class OrderDTO
    {
        public int Id { get; set; }
        public int CoproducerInfoId { get; set; }
        public string CoproducerName { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public string StatusName => Status.ToString();
        public decimal TotalAmount { get; set; }
        public int ItemCount { get; set; }
        public ICollection<string> ProducerNames { get; set; } = new List<string>();
    }
}
