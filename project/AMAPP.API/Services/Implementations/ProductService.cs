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

        public ProductService(IProductRepository productRepository, IMapper mapper, IProducerInfoRepository producerInfoRepository, UserManager<User> userManager)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _producerInfoRepository = producerInfoRepository;
            _userManager = userManager;
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

            // REPLACE your existing image validation with this secure validation:
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

            // REMOVE this old code block - no longer needed:
            // Convert the uploaded image to a byte array
            // byte[]? photoBytes = null;
            // if (productDto.Photo != null)
            // {
            //     using (var memoryStream = new MemoryStream())
            //     {
            //         await productDto.Photo.CopyToAsync(memoryStream);
            //         photoBytes = memoryStream.ToArray();
            //     }
            // }

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

            // Verificar se o usuário é administrador
            var user = await _userManager.FindByIdAsync(userId);
            var isAdmin = user != null && await _userManager.IsInRoleAsync(user, "Administrator");

            // Se não for administrador, verificar se é o proprietário
            if (!isAdmin)
            {
                bool isOwner = await IsProductOwnerAsync(id, userId);
                if (!isOwner)
                {
                    throw new UnauthorizedAccessException("You don't have permission to update this product.");
                }
            }

            // REPLACE your existing image validation with this secure validation:
            byte[]? photoBytes = existingProduct.Photo; // Keep existing photo by default
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

            // REMOVE this old code block - no longer needed:
            // if (productDto.Photo != null)
            // {
            //     if (productDto.Photo.Length > 5 * 1024 * 1024) // 5 MB limit
            //     {
            //         throw new ArgumentException("Photo size exceeds the 5MB limit.");
            //     }
            //
            //     var validFormats = new[] { ".jpg", ".jpeg", ".png" };
            //     var fileExtension = Path.GetExtension(productDto.Photo.FileName).ToLower();
            //     if (!validFormats.Contains(fileExtension))
            //     {
            //         throw new ArgumentException("Invalid photo format. Only JPG and PNG are allowed.");
            //     }
            // }

            // REMOVE this old code block too:
            // Convert the uploaded image to a byte array
            // byte[]? photoBytes = null;
            // if (productDto.Photo != null)
            // {
            //     using (var memoryStream = new MemoryStream())
            //     {
            //         await productDto.Photo.CopyToAsync(memoryStream);
            //         photoBytes = memoryStream.ToArray();
            //     }
            // }

            _mapper.Map(productDto, existingProduct);

            existingProduct.Photo = photoBytes;

            await _productRepository.UpdateAsync(existingProduct);

            return _mapper.Map<ProductDto>(existingProduct);
        }

        private async Task<bool> IsProductOwnerAsync(int productId, string userId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                return false;

            var producerInfo = await _producerInfoRepository.GetProducerInfoByUserIdAsync(userId);
            if (producerInfo == null)
                return false;

            return product.ProducerInfoId == producerInfo.Id;
        }

        public async Task<bool> DeleteProductAsync(int id, string userId)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return false;

            // Verificar se o usuário é administrador
            var user = await _userManager.FindByIdAsync(userId);
            var isAdmin = user != null && await _userManager.IsInRoleAsync(user, "Administrator");

            // Se não for administrador, verificar se é o proprietário
            if (!isAdmin)
            {
                bool isOwner = await IsProductOwnerAsync(id, userId);
                if (!isOwner)
                {
                    throw new UnauthorizedAccessException("You don't have permission to delete this product.");
                }
            }

            await _productRepository.RemoveAsync(product);

            return true;
        }

        //to validate if the product exists
        public async Task<ProductDto> GetProductDetailsByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return null;

            return _mapper.Map<ProductDto>(product);
        }
    }
}
