using JWT53.Dto.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JWT53.Services.seller;

public interface ISellerService
{
    Task<IEnumerable<UserDto>> GetAllSellersWithPropertyCountAsync();
    Task<UserDto> GetSellerWithPropertiesAsync(string sellerId);
}
