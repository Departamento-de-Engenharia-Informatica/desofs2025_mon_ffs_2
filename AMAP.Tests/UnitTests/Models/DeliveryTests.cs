using AMAPP.API.Models;
using Xunit;
using System;
using static AMAPP.API.Constants;

namespace AMAP.Tests.UnitTests.Models
{
    public class DeliveryTests
    {
        [Fact]
        public void CreateDelivery_ShouldSetAllPropertiesCorrectly()
        {
            // Arrange
            var order = new Order { Id = 1 };
            var delivery = new Delivery
            {
                Id = 10,
                OrderId = order.Id,
                Order = order,
                DeliveryDate = new DateTime(2025, 6, 1),
                DeliveryLocation = "Rua Principal 123",
                Status = DeliveryStatus.Scheduled
            };

            // Assert
            Assert.Equal(10, delivery.Id);
            Assert.Equal(1, delivery.OrderId);
            Assert.Equal(order, delivery.Order);
            Assert.Equal(new DateTime(2025, 6, 1), delivery.DeliveryDate);
            Assert.Equal("Rua Principal 123", delivery.DeliveryLocation);
            Assert.Equal(DeliveryStatus.Scheduled, delivery.Status);
        }
    }
}
