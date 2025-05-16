using static AMAPP.API.Constants;

namespace AMAPP.API.DTOs.Product
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double ReferencePrice { get; set; }
        public DeliveryUnit DeliveryUnit { get; set; } // Updated to use enum
        public int ProductTypeId { get; set; }
        public IFormFile? Photo { get; set; } // The uploaded image file

    }
}
