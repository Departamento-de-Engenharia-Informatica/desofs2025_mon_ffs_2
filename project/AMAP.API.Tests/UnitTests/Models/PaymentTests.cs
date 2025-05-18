using AMAPP.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AMAPP.API.Constants;

namespace AMAP.Tests.UnitTests.Models
{
    public class PaymentTests
    {
        [Fact]
        public void CreateValidPayment_ShouldSetAllPropertiesCorrectly()
        {
            // Arrange
            var order = new Order { Id = 1 }; // Supondo que existe uma classe Order
            var payment = new Payment
            {
                Id = 123, // <--- Adiciona esta linha para cobrir o Id
                OrderId = order.Id,
                Order = order,
                Amount = 49.99,
                PaymentDate = new DateTime(2025, 5, 17),
                PaymentMethod = PaymentMethod.MBWay,
                PaymentMode = PaymentMode.Full,
                Status = PaymentStatus.Completed
            };

            // Assert
            Assert.Equal(123, payment.Id); // <--- E esta linha para validar
            Assert.Equal(order.Id, payment.OrderId);
            Assert.Equal(order, payment.Order);
            Assert.Equal(49.99, payment.Amount);
            Assert.Equal(new DateTime(2025, 5, 17), payment.PaymentDate);
            Assert.Equal(PaymentMethod.MBWay, payment.PaymentMethod);
            Assert.Equal(PaymentMode.Full, payment.PaymentMode);
            Assert.Equal(PaymentStatus.Completed, payment.Status);
        }

    }
}
