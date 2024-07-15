using JWT53.Enum.Seller;
using Microsoft.AspNetCore.Identity;

namespace JWT53.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string ImageUrl { get; set; }

        public Status status { get; set; } 
        
        public DateTime CreatedDate { get; set; }
        public ICollection<Property> Properties { get; set; }


        public ICollection<PropertyLike> PropertyLikes { get; set; }
    }
}
