namespace JWT53.Dto.Property;

public class PropertyDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string MainImageUrl { get; set; } // رابط الصورة الرئيسية

    public string UserId { get; set; }
}
