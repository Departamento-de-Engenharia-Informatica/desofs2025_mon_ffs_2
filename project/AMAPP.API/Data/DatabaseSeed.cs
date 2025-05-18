using AMAPP.API.Models;
using Microsoft.AspNetCore.Identity;

namespace AMAPP.API.Data
{
    public static class DatabaseSeed
    {
        public static async Task SeedRolesAndUsers(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Ensure roles exist
            string[] roles = { "PROD", "COPR", "AMAP" , "ADMIN" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Seed a system admin user
            var systemAdminUser = await userManager.FindByNameAsync("admin");
            if (systemAdminUser == null)
            {
                systemAdminUser = new User
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    FirstName = "System",
                    LastName = "Administrator",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(systemAdminUser, "12345678");
                await userManager.AddToRoleAsync(systemAdminUser, "ADMIN");
            }


            // Seed a amap manager user
            var amapUser = await userManager.FindByNameAsync("tinoderans");
            if (amapUser == null)
            {
                amapUser = new User
                {
                    UserName = "tinoderans",
                    Email = "tinoderans@example.com",
                    FirstName = "Tino",
                    LastName = "de Rans",
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(amapUser, "12345678");
                await userManager.AddToRoleAsync(amapUser, "AMAP");
            }

            // Seed a Producer user
            var producerUser = await userManager.FindByNameAsync("quimbarreiros");
            if (producerUser == null)
            {
                producerUser = new User
                {
                    UserName = "quimbarreiros",
                    Email = "quimbarreiros@example.com",
                    FirstName = "Quim",
                    LastName = "Barreiros",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(producerUser, "12345678");
                await userManager.AddToRoleAsync(producerUser, "PROD");
            }

            // Seed a CoProducer user
            var coProducerUser = await userManager.FindByNameAsync("fernandomendes");
            if (coProducerUser == null)
            {
                coProducerUser = new User
                {
                    UserName = "fernandomendes",
                    Email = "fernandomendes@example.com",
                    FirstName = "Fernando",
                    LastName = "Mendes",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(coProducerUser, "12345678");
                await userManager.AddToRoleAsync(coProducerUser, "COPR");
            }


        }
    }
}
