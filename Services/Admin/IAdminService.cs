using JWT53.Dto.Auth;
using JWT53.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JWT53.Services.Admin
{
    public interface IAdminService
    {

        Task<IEnumerable<IdentityRole>> GetAllRolesAsync();
        Task<IEnumerable<ApplicationUser>> GetUsersInRoleAsync(string roleName);


        Task<IdentityResult> AddRoleAsync(string role);
        Task<IdentityResult> RemoveRoleAsync(string role);
        Task<IdentityResult> AssignRoleToUserAsync(UserRole model);
        Task<IdentityResult> RemoveRoleFromUserAsync(UserRole model);


    }
}
