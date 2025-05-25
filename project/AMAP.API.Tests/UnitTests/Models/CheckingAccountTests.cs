using AMAPP.API.Models;
using Xunit;

namespace AMAP.Tests.UnitTests.Models
{
    public class CheckingAccountTests
    {
        [Fact]
        public void CreateValidCheckingAccount_ShouldSetAllPropertiesCorrectly()
        {
            // Arrange
            var coproducer = new CoproducerInfo
            {
                Id = 1,
                UserId = "user123"
            };

            var account = new CheckingAccount
            {
                CoproducerId = coproducer.Id,
                Coproducer = coproducer,
                Balance = 150.75
            };

            // Assert
            Assert.Equal(1, account.CoproducerId);
            Assert.Equal(coproducer, account.Coproducer);
            Assert.Equal(150.75, account.Balance);
        }
    }
}
