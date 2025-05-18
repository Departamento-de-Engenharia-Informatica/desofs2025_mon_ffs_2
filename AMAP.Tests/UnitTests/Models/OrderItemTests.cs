using AMAPP.API.Models;
using Xunit;

namespace AMAP.Tests.UnitTests.Models
{
    public class OrderItemTests
    {
        [Fact]
        public void CreateValidOrderItem_ShouldSetAllPropertiesCorrectly()
        {
            // Arrange
            var order = new Order { Id = 1 };
            var product = new Product { Id = 10, Name = "Tomate" };

            var orderItem = new OrderItem
            {
                Id = 123, // Simular persistência
                OrderId = order.Id,
                Order = order,
                ProductId = product.Id,
                Product = product,
                Quantity = 5,
                Price = 2.50
            };

            // Assert
            Assert.Equal(123, orderItem.Id);
            Assert.Equal(1, orderItem.OrderId);
            Assert.Equal(order, orderItem.Order);
            Assert.Equal(10, orderItem.ProductId);
            Assert.Equal(product, orderItem.Product);
            Assert.Equal(5, orderItem.Quantity);
            Assert.Equal(2.50, orderItem.Price);
        }
    }
}
