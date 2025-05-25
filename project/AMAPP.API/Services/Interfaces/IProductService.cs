using AMAPP.API.DTOs.Product;

namespace AMAPP.API.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> AddProductAsync(CreateProductDto productDto, string userId);
        Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto productDto, string userId);
        Task<bool> DeleteProductAsync(int id, string userId);
    }
}
