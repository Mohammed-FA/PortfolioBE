using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Project.Domain.Entities;
using Project.Domain.Entities.Enm;

namespace Project.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<long>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<UserModel>>();
            var context = serviceProvider.GetRequiredService<AppDbContext>();

            string[] roleNames = new string[] { "Admin", "User" };

            foreach (var roleName in roleNames)
            {

                var roleExist = await roleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole<long>
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper()
                    });
                }
            }

            string EmailAdmin = "admin@admin.com";
            string adminPassword = "Admin123";

            var admin = await userManager.FindByEmailAsync(EmailAdmin);

            if (admin is null)
            {
                var adminUser = new UserModel
                {
                    UserName = "Admin",
                    Role = UserType.Admin,
                    Email = EmailAdmin,
                    FullName = "admin admin",
                    EmailConfirmed = true,

                };

                var createUser = await userManager.CreateAsync(adminUser, adminPassword);

                if (createUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
            string UserEmail = "mohamed@gmail.com";
            string UserPass = "Mohamed123";
            var usre = await userManager.FindByEmailAsync(UserEmail);


            if (usre is null)
            {
                var UserAccount = new UserModel
                {
                    UserName = "Abo_AlJohne",
                    Email = UserEmail,
                    Role = UserType.User,
                    FullName = "User User",
                    EmailConfirmed = true,


                };
                var results = await userManager.CreateAsync(UserAccount, UserPass);
                if (results.Succeeded)
                {
                    await userManager.AddToRoleAsync(UserAccount, "User");
                    await context.SaveChangesAsync();
                }

            }


        }
    }
}
