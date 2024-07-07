namespace JWT53.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName_Ar { get; set; }
        public string CategoryName_En { get; set; }
        public string CategoryName_Ku { get; set; }
        public string CategoryIconUrl { get; set; }

        // Navigation property
        public ICollection<Property> Properties { get; set; }
    }

}
