﻿using JWT53.Dto.File;

namespace JWT53.Dto.User;
public class UserDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string ImageUrl { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

}
