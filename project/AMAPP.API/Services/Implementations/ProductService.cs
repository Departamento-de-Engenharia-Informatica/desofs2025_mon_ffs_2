using AMAPP.API.DTOs.Product;
using AMAPP.API.Models;
using AMAPP.API.Repository.ProducerInfoRepository;
using AMAPP.API.Repository.ProdutoRepository;
using AMAPP.API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using AMAPP.API.Utils;


namespace AMAPP.API.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IProducerInfoRepository _producerInfoRepository;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ProductService> _logger;


        public ProductService(IProductRepository productRepository, IMapper mapper, IProducerInfoRepository producerInfoRepository, UserManager<User> userManager, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _producerInfoRepository = producerInfoRepository;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return null;

            return _mapper.Map<ProductDto>(product);
        }


        // Modificação para ProductService.cs
        public async Task<ProductDto> AddProductAsync(CreateProductDto productDto, string userId)
        {
            // Usamos o userId passado em vez do valor fixo "quimbarreiros"
            var producer = await _userManager.FindByIdAsync(userId);
            if (producer == null)
                throw new KeyNotFoundException("Producer not found.");

            byte[]? photoBytes = null;
            if (productDto.Photo != null)
            {
                try
                {
                    // This method does all the security validation and processing
                    photoBytes = await ImageSecurityHelper.ValidateAndProcessImageAsync(productDto.Photo);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException($"Image validation failed: {ex.Message}");
                }
            }

            // TODO: Remover
            var producerInfo = await _producerInfoRepository.GetProducerInfoByUserIdAsync(producer.Id);
            if (producerInfo == null)
            {
                producerInfo = new ProducerInfo
                {
                    UserId = producer.Id
                };

                await _producerInfoRepository.AddAsync(producerInfo);
            }


            var product = _mapper.Map<Product>(productDto);

            product.Photo = photoBytes;
            product.ProducerInfoId = producerInfo.Id;

            await _productRepository.AddAsync(product);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto productDto, string userId)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);

            if (existingProduct == null)
                throw new KeyNotFoundException("Product not found.");

            // Get user and check if admin
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            var isAdmin = await _userManager.IsInRoleAsync(user, "Administrator");

            // If not admin, MUST be the owner
            if (!isAdmin)
            {
                bool isOwner = await IsProductOwnerAsync(id, userId);
                if (!isOwner)
                {
                    throw new UnauthorizedAccessException("You don't have permission to update this product. You can only update products you created.");
                }
            }

            // Image validation and processing
            byte[]? photoBytes = existingProduct.Photo;
            if (productDto.Photo != null)
            {
                try
                {
                    photoBytes = await ImageSecurityHelper.ValidateAndProcessImageAsync(productDto.Photo);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException($"Image validation failed: {ex.Message}");
                }
            }

            _mapper.Map(productDto, existingProduct);
            existingProduct.Photo = photoBytes;

            await _productRepository.UpdateAsync(existingProduct);
            return _mapper.Map<ProductDto>(existingProduct);
        }


        private async Task<bool> IsProductOwnerAsync(int productId, string userId)
        {
            try
            {
                // Get the product with ProducerInfo included
                var product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                {
                    _logger?.LogWarning("Product {ProductId} not found when checking ownership", productId);
                    return false;
                }

                // Get the producer info for this user
                var producerInfo = await _producerInfoRepository.GetProducerInfoByUserIdAsync(userId);
                if (producerInfo == null)
                {
                    _logger?.LogWarning("ProducerInfo not found for user {UserId}", userId);
                    return false;
                }

                // Check if the product belongs to this producer
                bool isOwner = product.ProducerInfoId == producerInfo.Id;

                _logger?.LogInformation("Ownership check for Product {ProductId} by User {UserId}: {IsOwner}",
                    productId, userId, isOwner);

                return isOwner;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error checking product ownership for Product {ProductId} and User {UserId}",
                    productId, userId);
                return false; // Fail securely
            }
        }

        public async Task<bool> DeleteProductAsync(int id, string userId)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return false;

            // Get user and check if admin
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            var isAdmin = await _userManager.IsInRoleAsync(user, "Administrator");

            // If not admin, MUST be the owner
            if (!isAdmin)
            {
                bool isOwner = await IsProductOwnerAsync(id, userId);
                if (!isOwner)
                {
                    throw new UnauthorizedAccessException("You don't have permission to delete this product. You can only delete products you created.");
                }
            }

            await _productRepository.RemoveAsync(product);
            return true;
        }
    }
}
