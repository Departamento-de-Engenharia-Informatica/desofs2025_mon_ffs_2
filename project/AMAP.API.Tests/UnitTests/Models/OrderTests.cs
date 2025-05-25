using AMAPP.API.Models;
using Xunit;
using static AMAPP.API.Constants;
using System;
using System.Collections.Generic;

namespace AMAP.Tests.UnitTests.Models
{
    public class OrderTests
    {
        [Fact]
        public void CreateValidOrder_ShouldSetAllPropertiesCorrectly()
        {
            // Arrange
            var coproducer = new CoproducerInfo { Id = 1 };
            var orderItems = new List<OrderItem>
            {
                new OrderItem { Id = 1, Quantity = 2, Price = 5.0 },
                new OrderItem { Id = 2, Quantity = 1, Price = 10.0 }
            };
            var payment = new Payment
            {
                Id = 100,
                Amount = 20.0,
                PaymentDate = new DateTime(2025, 5, 17),
                PaymentMethod = PaymentMethod.Cash,
                PaymentMode = PaymentMode.Full,
                Status = PaymentStatus.Completed
            };
            var delivery = new Delivery
            {
                Id = 200,
                DeliveryDate = new DateTime(2025, 5, 20)
                // Address não incluído aqui
            };

            var order = new Order
            {
                Id = 50,
                CoproducerInfoId = coproducer.Id,
                CoproducerInfo = coproducer,
                OrderDate = new DateTime(2025, 5, 16),
                DeliveryRequirements = "Entregar no portão",
                Status = OrderStatus.Confirmed,
                OrderItems = orderItems
            };

            // Assert
            Assert.Equal(50, order.Id);
            Assert.Equal(1, order.CoproducerInfoId);
            Assert.Equal(coproducer, order.CoproducerInfo);
            Assert.Equal(new DateTime(2025, 5, 16), order.OrderDate);
            Assert.Equal("Entregar no portão", order.DeliveryRequirements);
            Assert.Equal(OrderStatus.Confirmed, order.Status);
            Assert.Equal(2, order.OrderItems.Count);
        }
    }
}
