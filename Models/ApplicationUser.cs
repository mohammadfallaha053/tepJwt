using Microsoft.AspNetCore.Identity;

namespace JWT53.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string ImageUrl { get; set; }
        public ICollection<Property> Properties { get; set; }

    }
}
