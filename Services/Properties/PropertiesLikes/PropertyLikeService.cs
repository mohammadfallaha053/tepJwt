using JWT53.Data;
using JWT53.Models;
using Microsoft.EntityFrameworkCore;

namespace JWT53.Services.Properties.PropertiesLikes;



public class PropertyLikeService : IPropertyLikeService
{
    private readonly ApplicationDbContext _context;

    public PropertyLikeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> TogglePropertyLikeAsync(string userId, Guid propertyId)
    {
        var propertyExists = await _context.Properties.AnyAsync(p => p.Id == propertyId);

        if (!propertyExists)
        {
            throw new KeyNotFoundException("Property not found");
        }


        var propertyLike = await _context.PropertyLikes
            .FirstOrDefaultAsync(pl => pl.UserId == userId && pl.PropertyId == propertyId);

        if (propertyLike == null)
        {
            // Add a new like
            propertyLike = new PropertyLike
            {
                UserId = userId,
                PropertyId = propertyId
            };
            _context.PropertyLikes.Add(propertyLike);
            await _context.SaveChangesAsync();
            return true; // Return true if a like was added
        }
        else
        {
            // Remove the existing like
            _context.PropertyLikes.Remove(propertyLike);
            await _context.SaveChangesAsync();
            return false; // Return false if a like was removed
        }
    }
}