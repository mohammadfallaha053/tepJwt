using Microsoft.AspNetCore.Identity;

namespace JWT53.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public ICollection<Property> Properties { get; set; }
    }
}
