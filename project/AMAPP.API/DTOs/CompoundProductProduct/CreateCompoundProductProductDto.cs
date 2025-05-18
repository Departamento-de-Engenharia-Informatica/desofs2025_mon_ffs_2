using System.ComponentModel.DataAnnotations;

namespace AMAPP.API.DTOs.CompoundProductProduct;
using AMAPP.API.DTOs.Product;

public class CreateCompoundProductProductDto
{
        [Required]
        public CreateProductDto CompoundProduct { get; set; }
        [Required]
        public int? ProductId { get; set; }
}