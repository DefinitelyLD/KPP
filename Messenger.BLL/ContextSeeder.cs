using Messenger.DAL.Entities;
using Messenger.DAL.UoW;
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
        private const string User = "User";
        private const string Admin = "Admin";

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(User).Result)
                roleManager.CreateAsync(new IdentityRole(User)).Wait();

            if (!roleManager.RoleExistsAsync(Admin).Result)
                roleManager.CreateAsync(new IdentityRole(Admin)).Wait();
        }

        public static void SeedUsers(IConfiguration configuration, UserManager<User> userManager, IUnitOfWork unitOfWork)
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

                var filePath = "/DefaultUserImage/image.png";

                UserImage imageEntity = new()
                {
                    Path = filePath,
                    UserId = user.Id
                };

                unitOfWork.UserImages.CreateAsync(imageEntity);

                userManager.CreateAsync(user, configuration["SuperUser:Password"]).Wait();
                userManager.AddToRoleAsync(user, Admin).Wait();
            }
        }
    }

}
