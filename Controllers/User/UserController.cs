using JWT53.Data;
using JWT53.Dto.User;
using JWT53.Models;
using JWT53.Services.Admin;
using JWT53.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JWT53.Controllers.User
{
  
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
       

       
        public UserController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
           
        }



        //[Authorize]
        [HttpPost("user/{userId}/upload-image")]
        public async Task<IActionResult> UploadUserProfileImage(string userId, IFormFile file)
        {
            try
            {
                await _userService.UpdateUserProfileImageAsync(userId, file);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        //[Authorize(Roles = "Admin")]
        [HttpGet("admin/count/total")]
        public async Task<IActionResult> GetTotalUsersCount()
        {
            var count = await _userService.GetTotalUsersCountAsync();
            return Ok(new { totalUsers = count });
        }

        [HttpGet("admin/count/by-role")]
        public async Task<IActionResult> GetUsersCountByRole([FromQuery] string roleName)
        {
            try
            {
                var count = await _userService.GetUsersCountByRoleAsync(roleName);
                return Ok(new { roleName = roleName, count = count });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }





        // [Authorize(Roles = "Admin")]
        [HttpDelete("admin/DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }


        //[Authorize]
        [HttpPut("user/update-user/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.PhoneNumber=model.PhoneNumber;
            user.FullName=model.FullName;


            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(user);
            }

            return BadRequest(result.Errors);
        }




       // [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
        {
            try
            {
                await _userService.ChangePasswordAsync(model.UserId, model.CurrentPassword, model.NewPassword);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }




        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            try
            {
                await _userService.ForgotPasswordAsync(model.Email);
                return Ok(new { message = "Password reset email sent." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            try
            {
                await _userService.ResetPasswordAsync(model.Email, model.Token, model.NewPassword);
                return Ok(new { message = "Password reset successful." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


       






        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            return Ok("ok");
        }



    }
}
