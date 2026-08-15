using learning_platform.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace learning_platform.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Define all required roles
            var roles = new[] { "SuperAdmin", "Admin", "Teacher", "Student" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Seed Super Admin
            var superAdminEmail = "superadmin@learningplatform.com";
            var superAdminUser = await userManager.FindByEmailAsync(superAdminEmail);

            if (superAdminUser == null)
            {
                var newSuperAdmin = new ApplicationUser
                {
                    UserName = superAdminEmail,
                    Email = superAdminEmail,
                    EmailConfirmed = true
                };

                var createResult = await userManager.CreateAsync(newSuperAdmin, "SuperAdmin@123");
                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(newSuperAdmin, "SuperAdmin");
                }
            }
        }
    }
}
