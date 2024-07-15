namespace JWT53.Services.Properties;

using JWT53.Dto.Property;
using JWT53.Models;
public interface IPropertyService
{
    Task<ResponsePropertyDto> GetPropertyByIdAsync(Guid id, string userId);
    Task<IEnumerable<ResponsePropertyDto>> GetAllPropertiesAsync(string userId);
    Task AddPropertyAsync(CreatePropertyDto createPropertyDto, string userId);
    Task UpdatePropertyAsync(Guid propertyId, UpdatePropertyDto dto);
    Task DeletePropertyAsync(Guid id);
}
