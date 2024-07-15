using AutoMapper;
using JWT53.Data;
using JWT53.Dto.Amenity;
using JWT53.Models;
using Microsoft.EntityFrameworkCore;

namespace JWT53.Services.Amenities;

public class AmenityService : IAmenityService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AmenityService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseAmenityDto> AddAmenityAsync(CreateAmenityDto AmenityDto)
    {
        var Amenity = _mapper.Map<Amenity>(AmenityDto);

        Amenity.Id = Guid.NewGuid();

        _context.Amenities.Add(Amenity);

        await _context.SaveChangesAsync();

        return _mapper.Map<ResponseAmenityDto>(Amenity);

    }

    public async Task<IEnumerable<ResponseAmenityDto>> GetAllAmenitiesAsync()
    {
        var Amenities = await _context.Amenities.ToListAsync();
        return _mapper.Map<IEnumerable<ResponseAmenityDto>>(Amenities);
    }

    public async Task<ResponseAmenityDto> GetAmenityByIdAsync(Guid id)
    {
        var Amenity = await _context.Amenities.FindAsync(id);
        if (Amenity == null)
        {
            throw new Exception("Amenity not found");
        }
        return _mapper.Map<ResponseAmenityDto>(Amenity);
    }

    public async Task<ResponseAmenityDto> UpdateAmenityAsync(Guid id, UpdateAmenityDto AmenityDto)
    {
        var Amenity = await _context.Amenities.FindAsync(id);
        if (Amenity == null)
        {
            throw new Exception("Amenity not found");
        }

        _mapper.Map(AmenityDto, Amenity);
        _context.Amenities.Update(Amenity);
        await _context.SaveChangesAsync();

        return _mapper.Map<ResponseAmenityDto>(Amenity);
    }

    public async Task DeleteAmenityAsync(Guid id)
    {
        var Amenity = await _context.Amenities.FirstOrDefaultAsync(c => c.Id == id);
        if (Amenity == null)
        {
            throw new Exception("Amenity not found");
        }

        _context.Amenities.Remove(Amenity);
        await _context.SaveChangesAsync();
    }

  
}
