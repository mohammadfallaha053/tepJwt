using JWT53.Models;
using JWT53.Services.User;
using Microsoft.AspNetCore.Identity;

namespace JWT53.Services.Buyer;

public class BuyerService : IBuyerService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public BuyerService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<IdentityResult> ConvertBuyerToSellerAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }

        // إزالة دور الشاري
        var removeBuyerRoleResult = await _userManager.RemoveFromRoleAsync(user, "Buyer");
        if (!removeBuyerRoleResult.Succeeded)
        {
            return removeBuyerRoleResult;
        }

        // إضافة دور البائع
        var addSellerRoleResult = await _userManager.AddToRoleAsync(user, "Seller");
        return addSellerRoleResult;
    }

}
