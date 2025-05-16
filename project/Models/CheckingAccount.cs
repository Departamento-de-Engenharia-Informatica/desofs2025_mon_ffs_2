using System.ComponentModel.DataAnnotations;

namespace AMAPP.API.Models
{
    public class CheckingAccount
    {
        [Key]
        public int CoproducerId { get; set; }
        public CoproducerInfo Coproducer { get; set; }

        public double Balance { get; set; }
    }
}
