namespace AMAPP.API.DTOs.Order
{
    public class OrderItemUpdateDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}