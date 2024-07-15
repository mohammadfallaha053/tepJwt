using JWT53.Data;
using JWT53.Dto.File;
using JWT53.Dto.User;
using JWT53.Models;
using JWT53.Services.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System;

namespace JWT53.Services.User;

public class UserService: IUserService
{
    private readonly long _maxImageSize = 2 * 1024 * 1024; // 2 ميجابايت
    private readonly List<string> _allowedImageExtensions = new List<string> { ".jpg", ".png", ".svg", ".jpeg" };

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
        _environment = environment;
    }


   

    public async Task<SellerDto> UpdateUserProfileImageAsync(string userId, IFormFile file)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        // Check for existing image and delete it
        if (!string.IsNullOrEmpty(user.ImageUrl))
        {
            var oldImagePath = Path.Combine("uploads", user.ImageUrl);
            if (File.Exists(oldImagePath))
            {
                File.Delete(oldImagePath);
            }
        }

        // Save new image
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        if (!_allowedImageExtensions.Contains(fileExtension) || file.Length > _maxImageSize)
        {
            throw new Exception("Invalid file type or size.");
        }

        var fileName = $"{Guid.NewGuid()}{fileExtension}";
        var filePath = Path.Combine("uploads", fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Update user's ImageUrl
        user.ImageUrl = fileName;
        await _userManager.UpdateAsync(user);

        return new SellerDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            ImageUrl = user.ImageUrl,
            PhoneNumber=user.PhoneNumber
        };
    }




    public async Task<int> GetTotalUsersCountAsync()
    {
        return await _userManager.Users.CountAsync();
    }


    public async Task<int> GetUsersCountByRoleAsync(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            throw new Exception($"Role '{roleName}' not found.");
        }

        var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
        return usersInRole.Count;
    }



    public async Task ForgotPasswordAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var callbackUrl = $"/reset-password?token={token}&email={email}";

        // إرسال البريد الإلكتروني مع رابط إعادة التعيين (callbackUrl)
        // يمكن استخدام خدمة بريد إلكتروني هنا
    }

    public async Task ResetPasswordAsync(string email, string token, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }








    public async Task<IdentityResult> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
         if (user == null || user.UserName.ToLower() == "admin")
            return IdentityResult.Failed(new IdentityError { Description = "Cannot delete admin or user not found" });

        // Delete user image if exists
        if (!string.IsNullOrEmpty(user.ImageUrl))
        {
            var imagePath = Path.Combine("uploads", user.ImageUrl);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }

        return await _userManager.DeleteAsync(user);
    }


    public async Task ChangePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
