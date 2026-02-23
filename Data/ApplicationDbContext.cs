using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AIHealthTestSystem.Data
{
    // IdentityDbContext ke saath Roles specify karna asaan rehta hai
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Future mein jab hum Models banayenge (e.g. Symptoms), to wo yahan DBSet ban kar ayenge
    }
}