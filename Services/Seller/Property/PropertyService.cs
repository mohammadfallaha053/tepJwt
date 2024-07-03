namespace JWT53.Services.Seller.Property;
using JWT53.Data;
using JWT53.Dto.Property;
using JWT53.Models;
using Microsoft.EntityFrameworkCore;
using System;

public class PropertyService : IPropertyService
    {
    private readonly ApplicationDbContext _context;

    public PropertyService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PropertyDto>> GetAllPropertiesAsync()
    {
        var properties = await _context.Properties.ToListAsync();
        var propertyDtos = new List<PropertyDto>();

        foreach (var property in properties)
        {
            var mainImage = _context.Files.FirstOrDefault(f => f.EntityId == property.Id.ToString() && f.EntityType == "Property");

            propertyDtos.Add(new PropertyDto
            {
                Id = property.Id,
                Name = property.Name,
                Price = property.Price,
                Description = property.Description,
                MainImageUrl = mainImage != null ? $"/uploads/{mainImage.FileName}" : null
            });
        }

        return propertyDtos;
    }

    public async Task<PropertyDto> GetPropertyByIdAsync(int id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property == null)
        {
            return null;
        }

        var propertyImages = _context.Files.Where(f => f.EntityId == property.Id.ToString() && f.EntityType == "Property").ToList();
        var propertyDto = new PropertyDto
        {
            Id = property.Id,
            Name = property.Name,
            Price = property.Price,
            Description = property.Description,
            MainImageUrl = propertyImages.Any() ? $"/uploads/{propertyImages.First().FileName}" : null
        };

        return propertyDto;
    }

    public async Task AddPropertyAsync(PropertyDto propertyDto)
    {
        var property = new Property
        {
            Name = propertyDto.Name,
            Price = propertyDto.Price,
            Description = propertyDto.Description,
            UserId = propertyDto.UserId
        };

        _context.Properties.Add(property);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePropertyAsync(PropertyDto propertyDto)
    {
        var property = await _context.Properties.FindAsync(propertyDto.Id);
        if (property != null)
        {
            property.Name = propertyDto.Name;
            property.Price = propertyDto.Price;
            property.Description = propertyDto.Description;

            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeletePropertyAsync(int id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property != null)
        {
            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();
        }
    }

    public Task AddPropertyAsync(Property property, string userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePropertyAsync(Property property)
    {
        throw new NotImplementedException();
    }
}
 

