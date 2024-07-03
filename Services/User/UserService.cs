using JWT53.Data;
using JWT53.Dto.File;
using JWT53.Dto.User;
using JWT53.Models;
using JWT53.Services.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System;

namespace JWT53.Services.User;

public class UserService: IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly IFileService _fileService;

    public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, IWebHostEnvironment environment, IFileService fileService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
        _environment = environment;
        _fileService = fileService;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersWithImagesAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var userDtos = new List<UserDto>();

        foreach (var user in users)
        {
            var files = await _fileService.GetFilesForEntityAsync("User", user.Id);
            var profileImages = files.Select(f => new FileDto { FileName = f.FileName, FilePath = f.FilePath }).ToList();

            userDtos.Add(new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
               ProfileImages = profileImages
            });
        }

        return userDtos;
    }


    
    public async Task<UserDto> GetUserWithImageAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return null;
        }

        var files = await _fileService.GetFilesForEntityAsync("User", userId);
        var profileImages = files.Select(f => new FileDto { FileName = f.FileName, FilePath = f.FilePath }).ToList();

        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            ProfileImages = profileImages
        };
    }




    public async Task AddUserImageAsync(string userId, IFormFile imageFile)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };

        var fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(fileExtension))
        {
            throw new Exception("Invalid file type. Only SVG,PNG,JPG are  allowed.");
        }


        await _fileService.SaveFilesAsync(new List<IFormFile> { imageFile }, "User", userId);
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





    //public async Task<IdentityResult> DeleteUserAsync(string userId)
    //{
    //    var user = await _userManager.FindByIdAsync(userId);
    //    if (user == null || user.UserName.ToLower() == "admin")
    //        return IdentityResult.Failed(new IdentityError { Description = "Cannot delete admin or user not found" });

    //    return await _userManager.DeleteAsync(user);
    //}


    public async Task<IdentityResult> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null || user.UserName.ToLower() == "admin")
            return IdentityResult.Failed(new IdentityError { Description = "Cannot delete admin or user not found" });

        // حذف الصور المرتبطة بالمستخدم
        var files = await _fileService.GetFilesForEntityAsync("User", userId);
        foreach (var file in files)
        {
            await _fileService.DeleteFileAsync(file.Id);
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
