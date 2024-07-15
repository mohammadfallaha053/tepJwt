using AutoMapper;
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
    private readonly IMapper _mapper;

    public CityService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseCityDto> AddCityAsync(CreateCityDto cityDto)
    {
        var city = _mapper.Map<City>(cityDto);

        city.Id = Guid.NewGuid();

        city.ImageUrl = "Soon/Soon";

        _context.Cities.Add(city);

        await _context.SaveChangesAsync();

        return _mapper.Map<ResponseCityDto>(city);

    }

    public async Task<IEnumerable<ResponseCityDto>> GetAllCitiesAsync()
    {
        var cities = await _context.Cities.ToListAsync();
        return _mapper.Map<IEnumerable<ResponseCityDto>>(cities);
    }

    public async Task<ResponseCityDto> GetCityByIdAsync(Guid id)
    {
        var city = await _context.Cities.FindAsync(id);
        if (city == null)
        {
            throw new Exception("City not found");
        }
        return _mapper.Map<ResponseCityDto>(city);
    }

    public async Task<ResponseCityDto> UpdateCityAsync(Guid id, UpdateCityDto cityDto)
    {
        var city = await _context.Cities.FindAsync(id);
        if (city == null)
        {
            throw new Exception("City not found");
        }

        _mapper.Map(cityDto, city);
        _context.Cities.Update(city);
        await _context.SaveChangesAsync();

        return _mapper.Map<ResponseCityDto>(city);
    }

    public async Task DeleteCityAsync(Guid id)
    {
        var city = await _context.Cities.Include(c => c.Properties).FirstOrDefaultAsync(c => c.Id == id);
        if (city == null)
        {
            throw new Exception("City not found");
        }

        if (city.Properties.Any())
        {
            throw new Exception("Cannot delete city with associated properties");
        }

        _context.Cities.Remove(city);
        await _context.SaveChangesAsync();
    }
}


