using JWT53.Dto.User;
using Microsoft.AspNetCore.Identity;

namespace JWT53.Services.User;

public interface IUserService
{



    Task<UserDto> GetUserWithImageAsync(string userId);

    Task<IdentityResult> DeleteUserAsync(string userId);

    Task<IEnumerable<UserDto>> GetAllUsersWithImagesAsync();

    Task AddUserImageAsync(string userId, IFormFile imageFile);




    Task ChangePasswordAsync(string userId, string currentPassword, string newPassword);
  
    
    Task ForgotPasswordAsync(string email);
    Task ResetPasswordAsync(string email, string token, string newPassword);
}
