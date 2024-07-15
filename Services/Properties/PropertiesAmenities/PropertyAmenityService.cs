using JWT53.Data;
using Microsoft.EntityFrameworkCore;

namespace JWT53.Services.Properties.PropertiesAmenities
{
    public class PropertyAmenityService : IPropertyAmenityService
    {
        private readonly ApplicationDbContext _context;

        public PropertyAmenityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAmenityToPropertyAsync(Guid propertyId, Guid amenityId)
        {
            var property = await _context.Properties.FindAsync(propertyId);
            var amenity = await _context.Amenities.FindAsync(amenityId);

            if (property == null)
                throw new Exception("Property not found");

            if (amenity == null)
                throw new Exception("Amenity not found");

            var existingLink = await _context.PropertyAmenities
                .FirstOrDefaultAsync(pa => pa.PropertyId == propertyId && pa.AmenityId == amenityId);

            if (existingLink != null)
                throw new Exception("Amenity already linked to property");

            var propertyAmenity = new Models.PropertyAmenity
            {
                PropertyId = propertyId,
                AmenityId = amenityId
            };

            _context.PropertyAmenities.Add(propertyAmenity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAmenityFromPropertyAsync(Guid propertyId, Guid amenityId)
        {
            var propertyAmenity = await _context.PropertyAmenities
                .FirstOrDefaultAsync(pa => pa.PropertyId == propertyId && pa.AmenityId == amenityId);

            if (propertyAmenity == null)
                throw new Exception("Amenity not linked to property");

            _context.PropertyAmenities.Remove(propertyAmenity);
            await _context.SaveChangesAsync();
        }
    }
}
