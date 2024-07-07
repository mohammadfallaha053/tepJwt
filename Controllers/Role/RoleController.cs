
using JWT53.Dto.Auth;
using JWT53.Models;
using JWT53.Services.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JWT53.Controllers.Role;

//[Authorize(Roles = "admin")]
[Route("api/admin")]
[ApiController]

public class RoleController : ControllerBase
{
    private readonly IAdminService _adminService;

    public RoleController(IAdminService adminService)
    {
        _adminService = adminService;
    }


    


    [HttpGet("get-all-roles")]
    public async Task<ActionResult<IEnumerable<IdentityRole>>> GetAllRole()
    {
        var roles = await _adminService.GetAllRolesAsync();
        return Ok(roles);
    }



    [HttpPost("add-role")]
    public async Task<IActionResult> AddRole([FromBody] string role)
    {
        var result = await _adminService.AddRoleAsync(role);
        if (result.Succeeded)
            return Ok(new { message = "Role added successfully" });

        return BadRequest(result.Errors);
    }



    [HttpDelete("delete-role")]
    public async Task<IActionResult> RemoveRole([FromBody] string role)
    {
        var result = await _adminService.RemoveRoleAsync(role);
        if (result.Succeeded)
            return Ok(new { message = "Role deleted successfully" });

        return BadRequest(result.Errors);
    }



    [HttpPost("assign-role-to-user")]
    public async Task<IActionResult> AssignRole([FromBody] UserRole model)
    {
        var result = await _adminService.AssignRoleToUserAsync(model);
        if (result.Succeeded)
            return Ok(new { message = "Role assigned successfully" });

        return BadRequest(result.Errors);
    }




    [HttpPost("remove-role-from-User")]
    public async Task<IActionResult> RemoveRoleFromUser([FromBody] UserRole model)
    {
        var result = await _adminService.RemoveRoleFromUserAsync(model);
        if (result.Succeeded)
            return Ok(new { message = "Role removed successfully" });

        return BadRequest(result.Errors);
    }
}
