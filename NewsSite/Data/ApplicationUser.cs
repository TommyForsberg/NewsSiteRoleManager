using Microsoft.AspNetCore.Identity;

namespace NewsSite.Data
{
    public class ApplicationUser : IdentityUser
    {
        public int Age { get; set; }
    }
}