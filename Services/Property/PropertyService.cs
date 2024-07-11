namespace JWT53.Services.Property;
using JWT53.Data;
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
        var property = new Property
        {   Id = Guid.NewGuid(),
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

    public async Task<ResponsePropertyDto> GetPropertyByIdAsync(Guid id)
    {
        var property = await _context.Properties
            .Include(p => p.Category)
            .Include(p => p.City)
            .Include(p => p.Files)
            .Include(p => p.User) // تضمين المستخدم في الاستعلام
            .FirstOrDefaultAsync(p => p.Id == id);

        if (property == null)
        {
            return null;
        }

        return new ResponsePropertyDto
        {
            Id = property.Id,
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
            CategoryId = property.CategoryId,
            CategoryName_Ar = property.Category.Name_Ar,
            CategoryName_En = property.Category.Name_En,
            CategoryName_Ku = property.Category.Name_Ku,
            CityId = property.CityId,
            CityName_Ar = property.City.Name_Ar,
            CityName_En = property.City.Name_En,
            CityName_Ku = property.City.Name_Ku,
            Files = property.Files.Select(f => new MyFileDto
            {
                FilePath = f.FilePath,
                FileName = f.FileName
            }).ToList(),
            UserFullName = property.User.FullName, // إضافة اسم المستخدم
            UserPhoneNumber = property.User.PhoneNumber // استخدام رقم هاتف المستخدم من IdentityUser
        };
    }

    public async Task<IEnumerable<ResponsePropertyDto>> GetAllPropertiesAsync()
    {
        var properties = await _context.Properties
            .Include(p => p.Category)
            .Include(p => p.City)
            .Include(p => p.User) // تضمين المستخدم في الاستعلام
            .ToListAsync();

        return properties.Select(property => new ResponsePropertyDto
        {
            Id = property.Id,
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
            CategoryId = property.CategoryId,
            CategoryName_Ar = property.Category.Name_Ar,
            CategoryName_En = property.Category.Name_En,
            CategoryName_Ku = property.Category.Name_Ku,
            CityId = property.CityId,
            CityName_Ar = property.City.Name_Ar,
            CityName_En = property.City.Name_En,
            CityName_Ku = property.City.Name_Ku,
            UserFullName = property.User.FullName, // إضافة اسم المستخدم
            UserPhoneNumber = property.User.PhoneNumber // استخدام رقم هاتف المستخدم من IdentityUser
        });
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

        _context.Properties.Update(property);
        await _context.SaveChangesAsync();
    }

}


