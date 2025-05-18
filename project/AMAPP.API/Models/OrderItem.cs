using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AMAPP.API.Models
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Relationship with Order
        public int OrderId { get; set; }
        public Order Order { get; set; }

        // Relationship with Product
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
