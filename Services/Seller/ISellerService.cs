using JWT53.Dto.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JWT53.Services.seller;

public interface ISellerService
{
    Task<IEnumerable<SellerDto>> GetAllSellersWithPropertyCountAsync();
    Task<SellerDto> GetSellerWithPropertiesAsync(string sellerId);
}
