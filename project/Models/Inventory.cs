using System.ComponentModel.DataAnnotations;

namespace AMAPP.API.Models
{
    public class Inventory
    {
        [Key]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int AvailableQuantity { get; set; }
        public DateTime AvailabilityDate { get; set; }
    }
}
