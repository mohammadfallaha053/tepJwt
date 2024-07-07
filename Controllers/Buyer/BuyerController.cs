using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JWT53.Models;
using JWT53.Services.Admin;
using System.ComponentModel.Design;
using JWT53.Services.Buyer;
namespace JWT53.Controllers.Buyer
{

   
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IBuyerService _buyerService;
        public BuyerController(IAdminService adminService, IBuyerService buyerService)
        {
            _adminService = adminService;
            _buyerService = buyerService;
        }



        [HttpGet("admin/get-all-buyers")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetBuyer()
        {
            var usersInRole = await _adminService.GetUsersInRoleAsync("buyer");
            return Ok(usersInRole);
        }



        [HttpPost("buyer/convert-to-seller")]
        public async Task<IActionResult> ConvertBuyerToSeller([FromBody] string userId)
        {
            var result = await _buyerService.ConvertBuyerToSellerAsync(userId);
            if (result.Succeeded)
            {
                return Ok(new { message = "User successfully converted to seller" });
            }

            return BadRequest(result.Errors);
        }



    }
}
