using JWT53.Enum;

namespace JWT53.Dto.Property;

public class CreatePropertyDto: BasePropertyDto
{
    public string UserId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid CityId { get; set; }
}
