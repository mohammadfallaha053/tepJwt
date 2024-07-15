using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JWT53.Services.Admin;
using JWT53.Models;
using JWT53.Services.seller;

namespace JWT53.Controllers;


[Route("api/")]
[ApiController]
public class SellerController : ControllerBase
{
    private readonly ISellerService _sellerService;

    public SellerController(ISellerService sellerService)
    {
        _sellerService = sellerService;
    }


    [HttpGet("users/sellers")]
    public async Task<IActionResult> GetAllSellersWithPropertyCount()
    {
        var sellers = await _sellerService.GetAllSellersWithPropertyCountAsync();
        return Ok(sellers);
    }



    [HttpGet("users/sellers/{sellerId}")]
    public async Task<IActionResult> GetSellerWithProperties(string sellerId)
    {
        var seller = await _sellerService.GetSellerWithPropertiesAsync(sellerId);
        if (seller == null)
        {
            return NotFound();
        }

        return Ok(seller);
    }
}
