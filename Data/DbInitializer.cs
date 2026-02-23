using Microsoft.AspNetCore.Identity;

namespace AIHealthTestSystem.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roleNames = { "Admin", "Patient" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                    if (!roleResult.Succeeded)
                    {
                        // Optional: throw to surface issues during seeding
                        var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                        throw new Exception($"Role create failed for '{roleName}': {errors}");
                    }
                }
            }

            // Create default Admin
            var adminEmail = "admin@healthai.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var createResult = await userManager.CreateAsync(adminUser, "Admin@123");

                if (!createResult.Succeeded)
                {
                    var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                    throw new Exception($"Admin user create failed: {errors}");
                }

                var roleAddResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                if (!roleAddResult.Succeeded)
                {
                    var errors = string.Join(", ", roleAddResult.Errors.Select(e => e.Description));
                    throw new Exception($"Admin role assign failed: {errors}");
                }
            }
            else
            {
                // Ensure Admin role assigned (in case user exists but role missing)
                if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                {
                    var roleAddResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                    if (!roleAddResult.Succeeded)
                    {
                        var errors = string.Join(", ", roleAddResult.Errors.Select(e => e.Description));
                        throw new Exception($"Admin role assign failed: {errors}");
                    }
                }
            }
        }
    }
}