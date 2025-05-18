using AMAPP.API.Models;
using Xunit;
using System;

namespace AMAP.Tests.UnitTests.Models
{
    public class InventoryTests
    {
        [Fact]
        public void CreateInventory_ShouldSetAllPropertiesCorrectly()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Cenoura" };
            var inventory = new Inventory
            {
                ProductId = product.Id,
                Product = product,
                AvailableQuantity = 30,
                AvailabilityDate = new DateTime(2025, 5, 21)
            };

            // Assert
            Assert.Equal(1, inventory.ProductId);
            Assert.Equal(product, inventory.Product);
            Assert.Equal(30, inventory.AvailableQuantity);
            Assert.Equal(new DateTime(2025, 5, 21), inventory.AvailabilityDate);
        }
    }
}
