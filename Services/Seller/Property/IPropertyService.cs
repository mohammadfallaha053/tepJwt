namespace JWT53.Services.Seller.Property;

using JWT53.Dto.Property;
using JWT53.Models;
public interface IPropertyService
{
    Task<PropertyDto> GetPropertyByIdAsync(int id);
    Task<IEnumerable<PropertyDto>> GetAllPropertiesAsync();
    Task AddPropertyAsync(Property property, string userId);
    Task UpdatePropertyAsync(Property property);
    Task DeletePropertyAsync(int id);
}
