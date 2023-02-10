using Microsoft.AspNetCore.Identity;

namespace StLawRestaurant.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
