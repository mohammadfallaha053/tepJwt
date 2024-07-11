using JWT53.Enum;
using JWT53.Models;

namespace JWT53.Dto.Property;

public class UpdatePropertyDto :BasePropertyDto
{
 
   
    public bool IsActive { get; set; }
    public bool IsShowInMainPage { get; set; }
    public bool IsShowInAdPage { get; set; }


}
