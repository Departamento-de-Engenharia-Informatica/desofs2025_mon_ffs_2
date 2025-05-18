using AMAPP.API.Models;
using Xunit;

namespace AMAP.Tests.UnitTests.Models
{
    public class AMAPAdministratorTests
    {
        [Fact]
        public void CreateValidAMAPAdministrator_ShouldSetAllPropertiesCorrectly()
        {
            // Arrange
            var user = new User
            {
                Id = "admin123",
                UserName = "admin@email.com"
            };

            var admin = new AMAPAdministrator
            {
                Id = 1,
                UserId = user.Id,
                User = user
            };

            // Assert
            Assert.Equal(1, admin.Id);
            Assert.Equal("admin123", admin.UserId);
            Assert.Equal(user, admin.User);
        }
    }
}
