using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JWT53.Services.Admin;
using JWT53.Models;
namespace JWT53.Controllers.Seller;


[Route("api/")]
[ApiController]
public class SellerController : ControllerBase
{
    private readonly IAdminService _adminService;

    public SellerController(IAdminService adminService)
    {
        _adminService = adminService;
    }
    

   // [Authorize(Roles = "admin")]
    [HttpGet("admin/get-all-sellers")]
    public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetAllSeller()
    {
        var usersInRole = await _adminService.GetUsersInRoleAsync("seller");
        return Ok(usersInRole);
    }

}
