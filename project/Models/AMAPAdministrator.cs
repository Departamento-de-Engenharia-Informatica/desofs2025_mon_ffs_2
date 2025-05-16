using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AMAPP.API.Models
{
    public class AMAPAdministrator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Relationship with User
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
