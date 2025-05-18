using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static AMAPP.API.Constants;

namespace AMAPP.API.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Relationship with Order
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
