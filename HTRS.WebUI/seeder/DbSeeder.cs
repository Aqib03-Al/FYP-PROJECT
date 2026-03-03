using HTRS.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace HTRS.WebUI
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // ✅ Roles banao
            string[] roles = { "Admin", "Patient" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // ✅ Default Admin banao
            string adminEmail = "admin@htrs.com";
            string adminPassword = "Admin@123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    FullName = "System Admin",
                    Email = adminEmail,
                    UserName = adminEmail,
                    Role = "Admin"
                };

                var result = await userManager.CreateAsync(user, adminPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}