using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JWT53.Models;
using JWT53.Services.Admin;
namespace JWT53.Controllers.Buyer
{
    
    [Route("api/")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public BuyerController(IAdminService adminService)
        {
            _adminService = adminService;
        }



        [Authorize(Roles = "admin")]
        [HttpGet("admin/get-all-buyers")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetBuyer()
        {
            var usersInRole = await _adminService.GetUsersInRoleAsync("buyer");
            return Ok(usersInRole);
        }
    }
}
