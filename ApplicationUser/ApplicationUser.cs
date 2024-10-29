using Microsoft.AspNetCore.Identity;

namespace JWT_Based_Login.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Add any custom properties you want here
        public string FullName { get; set; }
    }
}
