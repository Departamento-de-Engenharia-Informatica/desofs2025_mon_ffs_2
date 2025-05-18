using AMAPP.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMAP.Tests.UnitTests.Models
{
    public class UserTests
    {
        [Fact]
        public void User_CanSetAndGet_FirstName()
        {
            var user = new User { FirstName = "Quim" };
            Assert.Equal("Quim", user.FirstName);
        }

        [Fact]
        public void User_CanSetAndGet_LastName()
        {
            var user = new User { LastName = "Barreiros" };
            Assert.Equal("Barreiros", user.LastName);
        }

        [Fact]
        public void User_CanSetAndGet_Notifications()
        {
            var notifications = new List<Notification>
            {
                new Notification { Message = "Test 1" },
                new Notification { Message = "Test 2" }
            };

            var user = new User { Notifications = notifications };

            Assert.Equal(2, user.Notifications.Count);
            Assert.Contains(user.Notifications, n => n.Message == "Test 1");
            Assert.Contains(user.Notifications, n => n.Message == "Test 2");
        }
    }
}
