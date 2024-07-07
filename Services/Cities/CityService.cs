using JWT53.Data;
using JWT53.Dto.City;
using JWT53.Dto.Property;
using JWT53.Models;
using JWT53.Services.Cities;
using Microsoft.EntityFrameworkCore;

namespace JWT53.Services.Citeis;


public class CityService : ICityService
{

    private readonly ApplicationDbContext _context;

    public CityService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RsponseCityDto>> GetAllCitiesAsync()
    {
        return await _context.Cities
            .Select(city => new RsponseCityDto
            {
                Id = city.Id,
                Name_Ar = city.CityName_Ar,
                Name_En = city.CityName_En,
                Name_Ku = city.CityName_Ku,
                CityImageUrl = city.CityImageUrl,
                //Properties = city.Properties.Select(p => new PropertyDto
                //{
                //    Id = p.Id,
                //    Name_Ar = p.Name_Ar,
                //    Name_En = p.Name_En,
                //    Name_Ku = p.Name_Ku,
                //    // Add other property fields as needed
                //}).ToList()
            })
            .ToListAsync();
    }


    public async Task<RsponseCityDto> GetCityByIdAsync(int id)
    {
        var city = await _context.Cities
            .Include(c => c.Properties)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (city == null)
        {
            return null;
        }

        return new RsponseCityDto
        {
            Id = city.Id,
            Name_Ar = city.CityName_Ar,
            Name_En = city.CityName_En,
            Name_Ku = city.CityName_Ku,
            CityImageUrl = city.CityImageUrl,
            //Properties = city.Properties.Select(p => new PropertyDto
            //{
            //    Id = p.Id,
            //    Name_Ar = p.Name_Ar,
            //    Name_En = p.Name_En,
            //    Name_Ku = p.Name_Ku,
            //    // Add other property fields as needed
            //}).ToList()
        };
    }

    public async Task<City> AddCityAsync(CreateCityDto cityDto)
    {
        var city = new Models.City
        {
            CityName_Ar = cityDto.Name_Ar,
            CityName_En = cityDto.Name_En,
            CityName_Ku = cityDto.Name_Ku,
            CityImageUrl = "/Soon/Soon",
        };

        _context.Cities.Add(city);
        await _context.SaveChangesAsync();
        return city;
    }

    public async Task UpdateCityAsync(int id, UpdateCityDto cityDto)
    {
        var city = await _context.Cities.FindAsync(id);
        if (city == null)
        {
            throw new Exception("City not found");
        }

        city.CityName_Ar = cityDto.Name_Ar;
        city.CityName_En = cityDto.Name_En;
        city.CityName_Ku = cityDto.Name_Ku;
        

        _context.Cities.Update(city);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCityAsync(int id)
    {
        var city = await _context.Cities.FindAsync(id);
        if (city != null)
        {
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
        }
    }
}

