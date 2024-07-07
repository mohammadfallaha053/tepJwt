using JWT53.Dto.City;
using JWT53.Models;

namespace JWT53.Services.Cities;

public interface ICityService
{
    Task<IEnumerable<RsponseCityDto>> GetAllCitiesAsync();
    Task<RsponseCityDto> GetCityByIdAsync(int id);
    Task<City> AddCityAsync(CreateCityDto cityDto);
    Task UpdateCityAsync(int id, UpdateCityDto cityDto);
    Task DeleteCityAsync(int id);
}
