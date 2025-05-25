using System;
using System.Threading.Tasks;
using AMAPP.API.Data;
using AMAPP.API.DTOs.Product;
using AMAPP.API.Models;
using AMAPP.API.Repository.ProducerInfoRepository;
using AMAPP.API.Repository.ProdutoRepository;
using AMAPP.API.Services.Implementations;
using AMAPP.API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace AMAP.Tests.Integration
{
    public class SimpleProductTests : IDisposable
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly ProductService _productService;
        private readonly IProductRepository _productRepository;
        private readonly IProducerInfoRepository _producerInfoRepository;
        private readonly UserManager<User> _userManager;

        public SimpleProductTests()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));

            services.AddLogging();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAutoMapper(typeof(ProductService).Assembly);
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProducerInfoRepository, ProducerInfoRepository>();
            services.AddScoped<IProductService, ProductService>();

            _serviceProvider = services.BuildServiceProvider();
            _productService = (ProductService)_serviceProvider.GetRequiredService<IProductService>();
            _productRepository = _serviceProvider.GetRequiredService<IProductRepository>();
            _producerInfoRepository = _serviceProvider.GetRequiredService<IProducerInfoRepository>();
            _userManager = _serviceProvider.GetRequiredService<UserManager<User>>();
        }

        private async Task<User> CreateTestUser()
        {
            var user = new User
            {
                UserName = "test@example.com",
                Email = "test@example.com",
                FirstName = "Test",
                LastName = "User"
            };

            await _userManager.CreateAsync(user, "TestPassword123!");
            return user;
        }

        [Fact]
        public async Task CreateProduct_Success()
        {
            // Arrange
            var user = await CreateTestUser();
            var createDto = new CreateProductDto
            {
                Name = "Test Product",
                ReferencePrice = 10.0,
                Description = "Test Description"
            };

            // Act
            var result = await _productService.AddProductAsync(createDto, user.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Product", result.Name);
            Assert.Equal(10.0, result.ReferencePrice);
        }

        [Fact]
        public async Task UpdateProduct_Success()
        {
            // Arrange
            var user = await CreateTestUser();
            var producerInfo = new ProducerInfo { UserId = user.Id };
            await _producerInfoRepository.AddAsync(producerInfo);

            var product = new Product
            {
                Name = "Original Product",
                ReferencePrice = 10.0,
                ProducerInfoId = producerInfo.Id,
                DeliveryUnit = "kg", 
                Description = "Initial product description" 
            };
            await _productRepository.AddAsync(product);

            var updateDto = new UpdateProductDto
            {
                Name = "Updated Product",
                ReferencePrice = 20.0
            };

            // Act
            var result = await _productService.UpdateProductAsync(product.Id, updateDto, user.Id);

            // Assert
            Assert.Equal("Updated Product", result.Name);
            Assert.Equal(20.0, result.ReferencePrice);
        }

        [Fact]
        public async Task DeleteProduct_Success()
        {
            // Arrange
            var user = await CreateTestUser();
            var producerInfo = new ProducerInfo { UserId = user.Id };
            await _producerInfoRepository.AddAsync(producerInfo);

            var product = new Product
            {
                Name = "Product to Delete",
                ReferencePrice = 15.0,
                ProducerInfoId = producerInfo.Id,
                DeliveryUnit = "unit", 
                Description = "To be deleted" 
            };
            await _productRepository.AddAsync(product);

            // Act
            var result = await _productService.DeleteProductAsync(product.Id, user.Id);

            // Assert
            Assert.True(result);
        }


        public void Dispose()
        {
            _serviceProvider?.Dispose();
        }
    }
}