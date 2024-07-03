using JWT53.Dto.Auth;
using JWT53.Models;
using JWT53.Services.Admin;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT53.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<IEnumerable<IdentityRole>> GetAllRolesAsync()
        {
            return _roleManager.Roles.ToList();
        }


        public async Task<IEnumerable<ApplicationUser>> GetUsersInRoleAsync(string roleName)
        {
            return await _userManager.GetUsersInRoleAsync(roleName);
        }

        

        public async Task<IdentityResult> AddRoleAsync(string role)
        {
            if (await _roleManager.RoleExistsAsync(role))
                return IdentityResult.Failed(new IdentityError { Description = "Role already exists" });

            return await _roleManager.CreateAsync(new IdentityRole(role));
        }

        public async Task<IdentityResult> RemoveRoleAsync(string role)
        {
            var r = await _roleManager.FindByNameAsync(role);
            if (r == null)
                return IdentityResult.Failed(new IdentityError { Description = "Role not exists" });

            return await _roleManager.DeleteAsync(r);
        }

        public async Task<IdentityResult> AssignRoleToUserAsync(UserRole model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null || !await _roleManager.RoleExistsAsync(model.Role))
                return IdentityResult.Failed(new IdentityError { Description = "Invalid user ID or Role" });

            if (await _userManager.IsInRoleAsync(user, model.Role))
                return IdentityResult.Failed(new IdentityError { Description = "User already assigned to this role" });

            return await _userManager.AddToRoleAsync(user, model.Role);
        }

        public async Task<IdentityResult> RemoveRoleFromUserAsync(UserRole model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null || !await _roleManager.RoleExistsAsync(model.Role))
                return IdentityResult.Failed(new IdentityError { Description = "Invalid user ID or Role" });

            if (await _userManager.IsInRoleAsync(user, model.Role))
                return await _userManager.RemoveFromRoleAsync(user, model.Role);

            return IdentityResult.Failed(new IdentityError { Description = "User doesn't have this role" });
        }
    }
}
