using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static AMAPP.API.Constants;

namespace AMAPP.API.Models
{
    // Updated Order class with Reservation instead of Payment and Delivery
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Relationship with CoproducerInfo
        public int CoproducerInfoId { get; set; }
        public CoproducerInfo CoproducerInfo { get; set; }

        public DateTime OrderDate { get; set; }
        public string DeliveryRequirements { get; set; }
        public OrderStatus Status { get; set; }

        // Order items
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // Reservation for pickup/delivery
        public Reservation Reservation { get; set; }
    }

}