using JWT53.Data;
using JWT53.Dto.Category;
using JWT53.Dto.City;
using JWT53.Dto.File;
using JWT53.Dto.Property;
using JWT53.Dto.User;
using JWT53.Enum.Property;
using JWT53.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JWT53.Services.seller;

public class SellerService : ISellerService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;
    public SellerService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _context = context;
        
    }


    public async Task<IEnumerable<SellerDto>> GetAllSellersWithPropertyCountAsync()
    {
        var sellers = await _userManager.GetUsersInRoleAsync("Seller");
        var sellerDtos = sellers.Select(async seller
            => new SellerDto
            {
                Id = seller.Id,
                UserName = seller.UserName,
                Email = seller.Email,
                ImageUrl = seller.ImageUrl,
                PhoneNumber = seller.PhoneNumber,
                EmailConfirmed = seller.EmailConfirmed,
                CreatedDate = seller.CreatedDate,
                CountOfProperties = await _context.Properties.CountAsync(p => p.UserId == seller.Id),
                AvailableForSaleCount = await _context.Properties.CountAsync(p => p.UserId == seller.Id && p.Status == Status.Available && p.PropertyType == PropertyType.Sale),
                SoldCount = await _context.Properties.CountAsync(p => p.UserId == seller.Id && p.Status == Status.Unavailable && p.PropertyType == PropertyType.Sale),
                AvailableForRentCount = await _context.Properties.CountAsync(p => p.UserId == seller.Id && p.Status == Status.Available && p.PropertyType == PropertyType.Rent),
                RentedCount = await _context.Properties.CountAsync(p => p.UserId == seller.Id && p.Status == Status.Unavailable && p.PropertyType == PropertyType.Rent)
            }).Select(t => t.Result).ToList();

        return sellerDtos;
    }


    public async Task<SellerDto> GetSellerWithPropertiesAsync(string sellerId)
    {
        var seller = await _userManager.FindByIdAsync(sellerId);
        if (seller == null ||!(await _userManager.IsInRoleAsync(seller, "Seller")))
        {
            return null;
        }

        var properties = await _context.Properties
            .Where(p => p.UserId == sellerId)
            .Select(p => new ResponsePropertyDto
            {
                Id = p.Id,
                CreatedDate=p.CreatedDate,
                Name_Ar = p.Name_Ar,
                Name_En = p.Name_En,
                Name_Ku = p.Name_Ku,
                Description_Ar = p.Description_Ar,
                Description_En = p.Description_En,
                Description_Ku = p.Description_Ku,
                NumberOfRooms = p.NumberOfRooms,
                NumberOfBathrooms = p.NumberOfBathrooms,
                Lat=p.Lat,
                Long=p.Long,
                StreetName = p.StreetName,
                Area = p.Area,
                Price = p.Price,
                DiscountedPrice = p.DiscountedPrice,
                IsActive = p.IsActive,
                IsShowInMainPage = p.IsShowInMainPage,
                IsShowInAdPage = p.IsShowInAdPage,
                PropertyType = p.PropertyType,

                CityName = new BaseCityDto
                {
                    Name_Ar = p.City.Name_Ar,
                    Name_En = p.City.Name_En,
                    Name_Ku = p.City.Name_Ku,

                },
                CategoryName = new BaseCategoryDto
                {
                    Name_Ar = p.Category.Name_Ar,
                    Name_En = p.Category.Name_En,
                    Name_Ku = p.Category.Name_Ku,

                },
                Files = p.Files.Select(f => new MyFileDto
                {
                    FileName = f.FileName,
                    FilePath = f.FilePath,
                }).ToList()
            })
            .ToListAsync();

        return new SellerDto
        {
            Id = seller.Id,
            UserName = seller.UserName,
            Email = seller.Email,
            CreatedDate = seller.CreatedDate,
            //Role = "Seller",
            ImageUrl = seller.ImageUrl,
            PhoneNumber = seller.PhoneNumber,
            CountOfProperties = properties.Count,
            Properties = properties,
            EmailConfirmed = seller.EmailConfirmed,
            Status=seller.status,
        };
    }




}
