namespace JWT53.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name_Ar { get; set; }
        public string Name_En { get; set; }
        public string Name_Ku { get; set; }
        public string ImageUrl { get; set; }

        // Navigation property
        public ICollection<Property> Properties { get; set; }
    }

}
