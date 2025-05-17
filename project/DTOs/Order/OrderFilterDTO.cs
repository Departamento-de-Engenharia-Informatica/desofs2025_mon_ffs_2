using static AMAPP.API.Constants;

namespace AMAPP.API.DTOs.Order
{
    public class OrderFilterDTO
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public OrderStatus? Status { get; set; }
        public int? CoproducerId { get; set; }
        public int? ProducerId { get; set; }
        public int? ProductId { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; } = "desc";
    }
}
