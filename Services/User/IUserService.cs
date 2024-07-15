using JWT53.Dto.User;
using Microsoft.AspNetCore.Identity;

namespace JWT53.Services.User;

public interface IUserService
{



    Task<int> GetUsersCountByRoleAsync(string roleName);

    Task<int> GetTotalUsersCountAsync();

    Task<IdentityResult> DeleteUserAsync(string userId);



    Task<SellerDto> UpdateUserProfileImageAsync(string userId, IFormFile file);



    Task ChangePasswordAsync(string userId, string currentPassword, string newPassword);
  
    
    Task ForgotPasswordAsync(string email);
    Task ResetPasswordAsync(string email, string token, string newPassword);
}
