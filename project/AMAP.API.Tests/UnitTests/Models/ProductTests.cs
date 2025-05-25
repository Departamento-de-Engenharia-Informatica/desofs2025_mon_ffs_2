using Xunit;
using AMAPP.API.Models;

namespace AMAP.Tests.UnitTests.ProductTests
{
    public class ProductTests
    {
        [Fact]
        public void Product_CanSetAndGet_Properties()
        {
            // Arrange
            var producerInfo = new ProducerInfo { Id = 1 };
            var inventory = new Inventory();

            var product = new Product
            {
                Id = 1,
                Name = "Tomate",
                Description = "Tomate biológico",
                Photo = new byte[] { 1, 2, 3 },
                ReferencePrice = 1.99,
                DeliveryUnit = "kg",
                ProducerInfoId = 1,
                ProducerInfo = producerInfo,
                Inventory = inventory
            };

            // Act & Assert
            Assert.Equal(1, product.Id);
            Assert.Equal("Tomate", product.Name);
            Assert.Equal("Tomate biológico", product.Description);
            Assert.Equal(new byte[] { 1, 2, 3 }, product.Photo);
            Assert.Equal(1.99, product.ReferencePrice);
            Assert.Equal("kg", product.DeliveryUnit);
            Assert.Equal(1, product.ProducerInfoId);
            Assert.Equal(producerInfo, product.ProducerInfo);
            Assert.Equal(inventory, product.Inventory);
            Assert.Empty(product.OrderItems);
        }
    }
}
