using JWT53.Dto.City;
using JWT53.Models;

namespace JWT53.Services.Cities;

public interface ICityService
{
    //Task<IEnumerable<RsponseCityDto>> GetAllCitiesAsync();
    //Task<RsponseCityDto> GetCityByIdAsync(Guid id);
    //Task<RsponseCityDto> AddCityAsync(CreateCityDto cityDto);
    //Task UpdateCityAsync(Guid id, UpdateCityDto cityDto);
    //Task DeleteCityAsync(Guid id);


    Task<ResponseCityDto> AddCityAsync(CreateCityDto cityDto);
    Task<IEnumerable<ResponseCityDto>> GetAllCitiesAsync();
    Task<ResponseCityDto> GetCityByIdAsync(Guid id);
    Task UpdateCityAsync(Guid id, UpdateCityDto cityDto);
    Task DeleteCityAsync(Guid id);
}
