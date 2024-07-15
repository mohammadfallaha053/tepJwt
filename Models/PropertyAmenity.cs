namespace JWT53.Models
{
    public class PropertyAmenity
    {
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }

        public Guid AmenityId { get; set; }
        public Amenity Amenity { get; set; }
    }
}
