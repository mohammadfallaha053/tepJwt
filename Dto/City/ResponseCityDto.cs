using JWT53.Dto.Property;

namespace JWT53.Dto.City;

public class RsponseCityDto:BaseCityDto
{
    public int Id { get; set; }
   
    public string CityImageUrl { get; set; }

    // Navigation property
   // public ICollection<ResponsePropertyDto> Properties { get; set; }
}
