using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JWT53.Dto.Auth;
using JWT53.Models;

namespace JWT53.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Property> Properties { get; set; }

        public DbSet<MyFile> Files { get; set; }
    }
}
