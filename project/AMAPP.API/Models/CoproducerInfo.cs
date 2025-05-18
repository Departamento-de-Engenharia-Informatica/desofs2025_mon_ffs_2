using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMAPP.API.Models
{
    public class CoproducerInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Relacionamento com ApplicationUser
        public string UserId { get; set; }
        public User User { get; set; }

        // Orders placed by this co-producer
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        // Checking account
        public CheckingAccount CheckingAccount { get; set; }

    }
}
