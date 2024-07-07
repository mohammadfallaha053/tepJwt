using Microsoft.AspNetCore.Mvc;
using JWT53.Services.Cities;
using JWT53.Dto.City;
using Microsoft.AspNetCore.Authorization;
namespace JWT53.Controllers.City;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CityController : ControllerBase
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllCities()
    {
        var cities = await _cityService.GetAllCitiesAsync();
        return Ok(cities);
    }

    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetCityById(int id)
    {
        var city = await _cityService.GetCityByIdAsync(id);
        if (city == null)
        {
            return NotFound();
        }
        return Ok(city);
    }


    [HttpPost("add")]
    public async Task<IActionResult> AddCity([FromBody] CreateCityDto cityDto)
    {
        var NewCity= await _cityService.AddCityAsync(cityDto);
        return Ok(NewCity);
    }

    [HttpPut("edit/{id}")]
    public async Task<IActionResult> UpdateCity(int id, [FromBody] UpdateCityDto cityDto)
    {
        

        await _cityService.UpdateCityAsync(id, cityDto);
        return Ok("Successfully updated");
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteCity(int id)
    {
        await _cityService.DeleteCityAsync(id);
        return NoContent();
    }
}

