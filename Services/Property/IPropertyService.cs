namespace JWT53.Services.Property;

using JWT53.Dto.Property;
using JWT53.Models;
public interface IPropertyService
{
    Task<ResponsePropertyDto> GetPropertyByIdAsync(Guid id);
    Task<IEnumerable<ResponsePropertyDto>> GetAllPropertiesAsync();
    Task AddPropertyAsync(CreatePropertyDto createPropertyDto, string userId);
    Task UpdatePropertyAsync(Guid propertyId, UpdatePropertyDto dto);
    Task DeletePropertyAsync(Guid id);
}
