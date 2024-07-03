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


        
       // [Authorize]
        [HttpPost("{userId}/upload-image")]
        public async Task<IActionResult> UploadUserImage(string userId, IFormFile imageFile)
        {
            try
            {
                await _userService.AddUserImageAsync(userId, imageFile);
                return Ok("The image has been added successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        //[Authorize(Roles = "Admin")]
        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsersWithImages()
        {
            var users = await _userService.GetAllUsersWithImagesAsync();
            return Ok(users);
        }


        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetUserWithImage(string userId)
        {
            try
            {
                var user = await _userService.GetUserWithImageAsync(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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


        [Authorize]
        [HttpPut("user/update-user/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;


            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(user);
            }

            return BadRequest(result.Errors);
        }




        [Authorize]
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
