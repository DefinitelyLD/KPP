using Messenger.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL
{
    public static class ContextSeeder
    {
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
                roleManager.CreateAsync(new IdentityRole("User")).Wait();

            if (!roleManager.RoleExistsAsync("Admin").Result)
                roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
        }
        public static void SeedUsers(IConfiguration configuration, UserManager<User> userManager)
        {
            var mainModeratorEmail = configuration["SuperUser:Email"];
            var mainModeratorName = configuration["SuperUser:UserName"];

            if (userManager.FindByNameAsync(mainModeratorName).Result == null)
            {
                User user = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = mainModeratorName,
                    Email = mainModeratorEmail,
                    EmailConfirmed = true
                };

                userManager.CreateAsync(user, configuration["SuperUser:Password"]).Wait();
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }

}
