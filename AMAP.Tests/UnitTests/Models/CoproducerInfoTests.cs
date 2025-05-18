using AMAPP.API.Models;
using Xunit;
using System.Collections.Generic;

namespace AMAP.Tests.UnitTests.Models
{
    public class CoproducerInfoTests
    {
        [Fact]
        public void CreateCoproducerInfo_ShouldSetAllPropertiesCorrectly()
        {
            // Arrange
            var user = new User { Id = "user123" };
            var orders = new List<Order>
            {
                new Order { Id = 1 },
                new Order { Id = 2 }
            };
            var account = new CheckingAccount
            {
                Balance = 100.0
            };

            var coproducer = new CoproducerInfo
            {
                Id = 5,
                UserId = user.Id,
                User = user,
                Orders = orders,
                CheckingAccount = account
            };

            // Assert
            Assert.Equal(5, coproducer.Id);
            Assert.Equal("user123", coproducer.UserId);
            Assert.Equal(user, coproducer.User);
            Assert.Equal(2, coproducer.Orders.Count);
            Assert.Contains(coproducer.Orders, o => o.Id == 1);
            Assert.Contains(coproducer.Orders, o => o.Id == 2);
            Assert.Equal(account, coproducer.CheckingAccount);
        }

    }
}
