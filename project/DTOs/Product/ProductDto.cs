using AMAPP.API.Models;
using System.ComponentModel.DataAnnotations;

namespace AMAPP.API.DTOs.Product
{
    public class ProductDto
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public byte[]? Photo { get; set; }
        [Required]
        public double ReferencePrice { get; set; }
        [Required]
        public string DeliveryUnit { get; set; }
        public int ProductTypeId { get; set; } = 1;

    }
}
