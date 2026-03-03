using Microsoft.AspNetCore.Identity;

namespace HTRS.Infrastructure.Persistence
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}