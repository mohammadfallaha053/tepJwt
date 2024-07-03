using JWT53.Dto.File;

namespace JWT53.Dto.User;
public class UserDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string ProfilePictureUrl { get; set; } // رابط الصورة الشخصية
    public List<FileDto> ProfileImages { get; set; }

}
