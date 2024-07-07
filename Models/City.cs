namespace JWT53.Models
{
    public class City
    {
        public int Id { get; set; }
        public string CityName_Ar { get; set; }
        public string CityName_En { get; set; }
        public string CityName_Ku { get; set; }
        public string CityImageUrl { get; set; }

        // Navigation property
        public ICollection<Property> Properties { get; set; }
    }
}
