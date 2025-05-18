using static AMAPP.API.Constants;

namespace AMAPP.API.DTOs.Order
{
    public class OrderFilterDTO
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public OrderStatus? Status { get; set; }
        public int? ProductId { get; set; }
        public int? CoproducerId { get; set; }
        public int? ProducerId { get; set; }
        public string SortBy { get; set; } = "OrderDate"; // Default sorting
        public bool Descending { get; set; } = true; // Default descending order (newest first)
    }
}
