using AMAPP.API.Models;
using System;
using Xunit;

namespace AMAP.Tests.UnitTests.Models
{
    public class NotificationTests
    {
        [Fact]
        public void CreateNotification_ShouldSetAllPropertiesCorrectly()
        {
            var user = new User { Id = "user123" };
            var notification = new Notification
            {
                Id = 1,
                RecipientId = user.Id,
                Recipient = user,
                Message = "Mensagem de teste",
                Date = new DateTime(2025, 5, 18),
                IsRead = false
            };

            Assert.Equal(1, notification.Id);
            Assert.Equal("user123", notification.RecipientId);
            Assert.Equal(user, notification.Recipient);
            Assert.Equal("Mensagem de teste", notification.Message);
            Assert.Equal(new DateTime(2025, 5, 18), notification.Date);
            Assert.False(notification.IsRead);
        }

    }
}

