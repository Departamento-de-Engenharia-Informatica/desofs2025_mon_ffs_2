using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static AMAPP.API.Constants;

namespace AMAPP.API.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[]? Photo { get; set; }
        public double ReferencePrice { get; set; }
        public string DeliveryUnit { get; set; }

        // Relationship with Producer
        public int ProducerInfoId { get; set; }
        public ProducerInfo ProducerInfo { get; set; }

        // Inventory for this product
        public Inventory Inventory { get; set; }

        // Order items that include this product
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


    }
}
