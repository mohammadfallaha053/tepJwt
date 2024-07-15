using JWT53.Dto.Amenity;

namespace JWT53.Services.Amenities;

public interface IAmenityService
{
    Task<ResponseAmenityDto> AddAmenityAsync(CreateAmenityDto AmenityDto);
    Task<IEnumerable<ResponseAmenityDto>> GetAllAmenitiesAsync();
    Task<ResponseAmenityDto> GetAmenityByIdAsync(Guid id);
   Task<ResponseAmenityDto>UpdateAmenityAsync(Guid id, UpdateAmenityDto AmenityDto);
    Task DeleteAmenityAsync(Guid id);
}
