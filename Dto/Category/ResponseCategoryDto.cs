using JWT53.Dto.Property;

namespace JWT53.Dto.Category
{
public class ResponseCategoryDto:BaseCategoryDto
{
    public int Id { get; set; }
    public string CategoryIconUrl { get; set; }

    // Navigation property
    //public ICollection<ResponsePropertyDto> Properties { get; set; }
}
}
