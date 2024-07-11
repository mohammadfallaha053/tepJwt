using JWT53.Dto.Property;

namespace JWT53.Dto.City;

public class ResponseCityDto:BaseCityDto
{
    public Guid Id { get; set; }
   
    public string ImageUrl { get; set; }

    // Navigation property
   // public ICollection<ResponsePropertyDto> Properties { get; set; }
}
