namespace AMAPP.API.DTOs.Order
{
    public class OrderItemCreateDTO
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}
