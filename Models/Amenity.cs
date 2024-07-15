namespace JWT53.Models;

public class Amenity
{
    public Guid Id { get; set; }
    public string Name_Ar { get; set; }
    public string Name_En { get; set; }
    public string Name_Ku { get; set; }

    public ICollection<PropertyAmenity> PropertyAmenities { get; set; }
}
