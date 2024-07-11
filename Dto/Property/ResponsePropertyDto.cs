using JWT53.Dto.File;
using JWT53.Enum;
using JWT53.Models;

namespace JWT53.Dto.Property;

public class ResponsePropertyDto : BasePropertyDto
{
    public Guid Id { get; set; }

    public bool IsActive { get; set; }          
    public bool IsShowInMainPage { get; set; }
    public bool IsShowInAdPage { get; set; }
    public PropertyType PropertyType { get; set; } // تحديث إلى enum

    public Guid CategoryId { get; set; }
    public string CategoryName_Ar { get; set; }
    public string CategoryName_En { get; set; }
    public string CategoryName_Ku { get; set; }

    public Guid CityId { get; set; }
    public string CityName_Ar { get; set; }
    public string CityName_En { get; set; }
    public string CityName_Ku { get; set; }
   
    public string UserFullName { get; set; }
    public string UserPhoneNumber { get; set;}
    public ICollection<MyFileDto> Files { get; set;}





}

