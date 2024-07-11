using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JWT53.Dto.Auth;
using JWT53.Models;

namespace JWT53.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<City> Cities { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Property> Properties { get; set; }

    public DbSet<MyFile> Files { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<City>()
            .Property(c => c.Id)
            .HasDefaultValueSql("NEWID()");

        modelBuilder.Entity<Category>()
            .Property(c => c.Id)
            .HasDefaultValueSql("NEWID()");

        modelBuilder.Entity<Property>()
            .HasOne(p => p.City)
            .WithMany(c => c.Properties)
            .HasForeignKey(p => p.CityId)
            .OnDelete(DeleteBehavior.Restrict); // تعيين Restrict بدلاً من Cascade

        modelBuilder.Entity<Property>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Properties)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); // تعيين Restrict بدلاً من Cascade
    }

}