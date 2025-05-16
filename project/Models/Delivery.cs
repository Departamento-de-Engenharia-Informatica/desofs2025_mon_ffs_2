using static AMAPP.API.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AMAPP.API.Models
{
    public class Delivery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Relationship with Order
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public DateTime DeliveryDate { get; set; }
        public string DeliveryLocation { get; set; }
        public DeliveryStatus Status { get; set; }
    }
}
