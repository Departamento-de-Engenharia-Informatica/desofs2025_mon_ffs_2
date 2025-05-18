using AMAPP.API.Models;
using Xunit;

namespace AMAPP.Tests.Unit
{
    public class ProducerInfoTests
    {
        [Fact]
        public void ProducerInfo_ShouldSetAndGetIdAndUserId()
        {
            // Arrange
            var producer = new ProducerInfo
            {
                Id = 10,
                UserId = "user-001"
            };

            // Assert
            Assert.Equal(10, producer.Id);
            Assert.Equal("user-001", producer.UserId);
        }

        [Fact]
        public void ProducerInfo_ShouldSetAndGetUser()
        {
            // Arrange
            var user = new User { Id = "user-001", UserName = "teste@amap.com" };
            var producer = new ProducerInfo
            {
                UserId = user.Id,
                User = user
            };

            // Assert
            Assert.Equal("user-001", producer.UserId);
            Assert.Equal(user, producer.User);
            Assert.Equal("teste@amap.com", producer.User.UserName);
        }

        [Fact]
        public void ProducerInfo_ShouldAddProductToProductList()
        {
            // Arrange
            var product = new Product { Name = "Abóbora", DeliveryUnit = "kg", ReferencePrice = 1.5 };
            var producer = new ProducerInfo();

            // Act
            producer.Products.Add(product);

            // Assert
            Assert.Single(producer.Products);
            Assert.Equal("Abóbora", producer.Products[0].Name);
        }
    }
}
