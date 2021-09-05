using BugTracking.DAL.Data;
using BugTracking.DAL.Entities;
using BugTracking.Models;
using BugTracking.Services;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishCards.Services
{
    /// <summary>
    /// Initial DB state and default admin user.
    /// </summary>
    public class DbInitializer
    {
        public static async Task Seed(ApplicationDbContext context, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, IUserService<UserModel, UserRoleModel> userService)
        {
            // создать БД, если она еще не создана
            context.Database.EnsureCreated();
            // проверка наличия ролей
            if (!context.Roles.Any())
            {
                var roleAdmin = new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin"
                };

                // создать роль admin
                await roleManager.CreateAsync(roleAdmin);
            }
            // проверка наличия пользователей
            if (!context.Users.Any())
            {
                // создать пользователя chynayeua@yandex.ru
                var user = new ApplicationUser
                {
                    Email = "chynayeu.90331@tut.by",
                    UserName = "chynayeu.90331@tut.by",
                };
                await userManager.CreateAsync(user, "Test123");
                user = await userManager.FindByEmailAsync("chynayeu.90331@tut.by");
                await userManager.AddToRoleAsync(user, "admin");

                List<UserRoleModel> roles = new List<UserRoleModel>();
                roles.Add(new UserRoleModel("admin"));
                userService.CreateUser("Aliaksei", "Chynayeu", "chynayeu.90331@tut.by", roles);

            }
                await context.SaveChangesAsync();
        }
    }
}
