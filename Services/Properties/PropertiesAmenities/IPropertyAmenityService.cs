namespace JWT53.Services.Properties.PropertiesAmenities;

public interface IPropertyAmenityService
{
    Task AddAmenityToPropertyAsync(Guid propertyId, Guid amenityId);
    Task RemoveAmenityFromPropertyAsync(Guid propertyId, Guid amenityId);
}
