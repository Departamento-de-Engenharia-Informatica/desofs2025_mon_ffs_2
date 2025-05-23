﻿using Microsoft.AspNetCore.Identity;

namespace AMAPP.API.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Notification> Notifications { get; set; }
    }
}
