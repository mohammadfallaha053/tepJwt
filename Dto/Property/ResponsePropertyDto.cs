using JWT53.Dto.Amenity;
using JWT53.Dto.Category;
using JWT53.Dto.City;
using JWT53.Dto.File;
using JWT53.Enum.Property;
using JWT53.Models;

namespace JWT53.Dto.Property;

public class ResponsePropertyDto : BasePropertyDto
{
    public Guid Id { get; set; }

    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }          
    public bool IsShowInMainPage { get; set; }
    public bool IsShowInAdPage { get; set; }
   


    public BaseCityDto CityName { get; set; }
    public BaseCategoryDto CategoryName { get; set; }

   
    public string UserFullName { get; set; }
    public string UserPhoneNumber { get; set;}
    public ICollection<MyFileDto> Files { get; set;}

    public ICollection<BaseAmenityDto> Amenities { get; set; } // 


    public int LikesCount { get; set; }
    public bool IsLike { get; set; } = false; // تعيين القيمة الافتراضية لـ IsLike كـ false


}

