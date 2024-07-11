using JWT53.Dto.Property;

namespace JWT53.Dto.Category
{
public class ResponseCategoryDto:BaseCategoryDto
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; }

    // Navigation property
    //public ICollection<ResponsePropertyDto> Properties { get; set; }
}
}
