using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMAPP.API.Models;

public class ProducerInfo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    // Relacionamento com ApplicationUser
    public string UserId { get; set; }
    public User User { get; set; }

    // Propriedade específica
    public List<Product> Products { get; set; } = new List<Product>();
}
