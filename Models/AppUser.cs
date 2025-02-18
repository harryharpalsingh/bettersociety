using Microsoft.AspNetCore.Identity;

namespace bettersociety.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }  // Example additional property
    }
}
