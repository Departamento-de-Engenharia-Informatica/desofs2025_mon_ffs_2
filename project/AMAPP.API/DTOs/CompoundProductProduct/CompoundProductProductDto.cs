using System.Collections.Generic;
using AMAPP.API.Models;
using AMAPP.API.DTOs.Product;

namespace AMAPP.API.DTOs.CompoundProductProduct
{
    public class CompoundProductProductDto
    {
        public int CompoundProductId { get; set; }
        public ProductDto CompoundProduct { get; set; }
        public int? ProductId { get; set; }
        public ProductDto? Product { get; set; }
    }
}