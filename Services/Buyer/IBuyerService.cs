using Microsoft.AspNetCore.Identity;

namespace JWT53.Services.Buyer;

public interface IBuyerService
{

    Task<IdentityResult> ConvertBuyerToSellerAsync(string userId);
}
