namespace JWT53.Services.Properties;
using JWT53.Data;
using JWT53.Dto.Amenity;
using JWT53.Dto.Category;
using JWT53.Dto.City;
using JWT53.Dto.File;
using JWT53.Dto.Property;
using JWT53.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

public class PropertyService : IPropertyService
{
    private readonly ApplicationDbContext _context;

    public PropertyService(ApplicationDbContext context)
    {
        _context = context;
    }
    

    public async Task AddPropertyAsync(CreatePropertyDto createPropertyDto, string userId)
    {
        TimeZoneInfo damascusTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Syria Standard Time");
        var date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, damascusTimeZone);

        var property = new Property
        {   Id = Guid.NewGuid(),
            CreatedDate = date,
            Name_Ar = createPropertyDto.Name_Ar,
            Name_En = createPropertyDto.Name_En,
            Name_Ku = createPropertyDto.Name_Ku,
            Description_Ar = createPropertyDto.Description_Ar,
            Description_En = createPropertyDto.Description_En,
            Description_Ku = createPropertyDto.Description_Ku,
            NumberOfRooms = createPropertyDto.NumberOfRooms,
            NumberOfBathrooms = createPropertyDto.NumberOfBathrooms,
            Lat = createPropertyDto.Lat,
            Long = createPropertyDto.Long,
            StreetName = createPropertyDto.StreetName,
            Area = createPropertyDto.Area,
            Price = createPropertyDto.Price,
            DiscountedPrice = createPropertyDto.DiscountedPrice,
            PropertyType = createPropertyDto.PropertyType,
            CategoryId = createPropertyDto.CategoryId,
            CityId = createPropertyDto.CityId,
            UserId = userId,
            IsActive = false, // افتراضياً غير مفعل حتى يتم تفعيله من قبل الادمن
            IsShowInMainPage = false, // افتراضياً غير معروض في الصفحة الرئيسية
            IsShowInAdPage = false // افتراضياً غير معروض في صفحة الإعلان
        };

        _context.Properties.Add(property);

        await _context.SaveChangesAsync();

       
    }

    public async Task DeletePropertyAsync(Guid id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property == null)
        {
            throw new KeyNotFoundException("Property not found.");
        }

        _context.Properties.Remove(property);
        await _context.SaveChangesAsync();
    }





    public async Task<ResponsePropertyDto> GetPropertyByIdAsync(Guid id, string userId)
    {
        var property = await _context.Properties
            .Include(p => p.Category)
            .Include(p => p.City)
            .Include(p => p.Files)
            .Include(p => p.User)
            .Include(p => p.PropertyAmenities)
                .ThenInclude(pa => pa.Amenity) // تضمين وسائل الراحة المرتبطة
            .FirstOrDefaultAsync(p => p.Id == id);

        if (property == null)
        {
            return null;
        }

        // الحصول على عدد الإعجابات
        var likesCount = await _context.PropertyLikes.CountAsync(pl => pl.PropertyId == id);

        // التحقق إذا كان المستخدم قد أعجب بالعقار
        var isLikedByUser = await _context.PropertyLikes.AnyAsync(pl => pl.PropertyId == id && pl.UserId == userId);

        return new ResponsePropertyDto
        {
            Id = property.Id,
            CreatedDate = property.CreatedDate,
            Name_Ar = property.Name_Ar,
            Name_En = property.Name_En,
            Name_Ku = property.Name_Ku,
            Description_Ar = property.Description_Ar,
            Description_En = property.Description_En,
            Description_Ku = property.Description_Ku,
            NumberOfRooms = property.NumberOfRooms,
            NumberOfBathrooms = property.NumberOfBathrooms,
            Lat = property.Lat,
            Long = property.Long,
            StreetName = property.StreetName,
            Area = property.Area,
            Price = property.Price,
            DiscountedPrice = property.DiscountedPrice,
            IsActive = property.IsActive,
            IsShowInMainPage = property.IsShowInMainPage,
            IsShowInAdPage = property.IsShowInAdPage,
            PropertyType = property.PropertyType,
            CityName = new BaseCityDto
            {
                Name_Ar = property.City.Name_Ar,
                Name_En = property.City.Name_En,
                Name_Ku = property.City.Name_Ku,
            },
            CategoryName = new BaseCategoryDto
            {
                Name_Ar = property.Category.Name_Ar,
                Name_En = property.Category.Name_En,
                Name_Ku = property.Category.Name_Ku,
            },
            Files = property.Files.Select(f => new MyFileDto
            {
                FilePath = f.FilePath,
                FileName = f.FileName
            }).ToList(),
            UserFullName = property.User.FullName,
            UserPhoneNumber = property.User.PhoneNumber,
            Amenities = property.PropertyAmenities.Select(pa => new BaseAmenityDto
            {
                Name_Ar = pa.Amenity.Name_Ar,
                Name_En = pa.Amenity.Name_En,
                Name_Ku = pa.Amenity.Name_Ku,
            }).ToList(),
            LikesCount = likesCount, // تضمين عدد الإعجابات
            IsLike = isLikedByUser // تضمين حالة الإعجاب
        };
    }

    public async Task<IEnumerable<ResponsePropertyDto>> GetAllPropertiesAsync(string userId)
    {
        var properties = await _context.Properties
            .Include(p => p.Category)
            .Include(p => p.City)
            .Include(p => p.User) // تضمين المستخدم في الاستعلام
            .ToListAsync();

        var propertyIds = properties.Select(p => p.Id).ToList();

        // الحصول على عدد الإعجابات لكل عقار
        var likesCounts = await _context.PropertyLikes
            .Where(pl => propertyIds.Contains(pl.PropertyId))
            .GroupBy(pl => pl.PropertyId)
            .Select(g => new
            {
                PropertyId = g.Key,
                Count = g.Count()
            }).ToDictionaryAsync(g => g.PropertyId, g => g.Count);

        // الحصول على العقارات التي أعجب بها المستخدم
        var userLikes = await _context.PropertyLikes
            .Where(pl => pl.UserId == userId && propertyIds.Contains(pl.PropertyId))
            .Select(pl => pl.PropertyId)
            .ToListAsync();

        return properties.Select(property => new ResponsePropertyDto
        {
            Id = property.Id,
            CreatedDate = property.CreatedDate,
            Name_Ar = property.Name_Ar,
            Name_En = property.Name_En,
            Name_Ku = property.Name_Ku,
            Description_Ar = property.Description_Ar,
            Description_En = property.Description_En,
            Description_Ku = property.Description_Ku,
            NumberOfRooms = property.NumberOfRooms,
            NumberOfBathrooms = property.NumberOfBathrooms,
            Lat = property.Lat,
            Long = property.Long,
            StreetName = property.StreetName,
            Area = property.Area,
            Price = property.Price,
            DiscountedPrice = property.DiscountedPrice,
            IsActive = property.IsActive,
            IsShowInMainPage = property.IsShowInMainPage,
            IsShowInAdPage = property.IsShowInAdPage,
            PropertyType = property.PropertyType, // تحديث إلى enum
            CategoryName = new BaseCategoryDto
            {
                Name_Ar = property.Category.Name_Ar,
                Name_En = property.Category.Name_En,
                Name_Ku = property.Category.Name_Ku
            },
            CityName = new BaseCityDto
            {
                Name_Ar = property.City.Name_Ar,
                Name_En = property.City.Name_En,
                Name_Ku = property.City.Name_Ku
            },
            UserFullName = property.User.FullName, // إضافة اسم المستخدم
            UserPhoneNumber = property.User.PhoneNumber, // استخدام رقم هاتف المستخدم من IdentityUser
            LikesCount = likesCounts.TryGetValue(property.Id, out var count) ? count : 0, // تعيين عدد الإعجابات
            IsLike = userLikes.Contains(property.Id) // تعيين ما إذا كان المستخدم قد أعجب بالعقار
        }).ToList();
    }


    public async Task UpdatePropertyAsync(Guid propertyId, UpdatePropertyDto dto)
    {
        var property = await _context.Properties.FindAsync(propertyId);
        if (property == null)
        {
            throw new Exception("Property not found");
        }

        property.Name_Ar = dto.Name_Ar;
        property.Name_En = dto.Name_En;
        property.Name_Ku = dto.Name_Ku;
        property.Description_Ar = dto.Description_Ar;
        property.Description_En = dto.Description_En;
        property.Description_Ku = dto.Description_Ku;
        property.NumberOfRooms = dto.NumberOfRooms;
        property.NumberOfBathrooms = dto.NumberOfBathrooms;
        property.Area = dto.Area;
        property.Price = dto.Price;
        property.DiscountedPrice = dto.DiscountedPrice;
        property.PropertyType = dto.PropertyType;
        property.CategoryId = dto.CategoryId;
        property.CityId = dto.CityId;
        property.IsActive = dto.IsActive;
        property.IsShowInMainPage = dto.IsShowInMainPage;
        property.IsShowInAdPage = dto.IsShowInAdPage;
        property.Lat = dto.Lat;
        property.Long = dto.Long;
        property.StreetName = dto.StreetName;
        property.Status= dto.Status;
        _context.Properties.Update(property);
        await _context.SaveChangesAsync();



       
    }



}


