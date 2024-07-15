using Microsoft.AspNetCore.Mvc;
using JWT53.Services.Cities;
using JWT53.Dto.City;
using Microsoft.AspNetCore.Authorization;
using JWT53.Services.Citeis;
namespace JWT53.Controllers;
//[Authorize]
[ApiController]
[Route("api/[controller]")]

public class CityController : ControllerBase
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddCity([FromBody] CreateCityDto cityDto)
    {
        return Ok(await _cityService.AddCityAsync(cityDto));
    }



    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllCities()
    {

        return Ok(await _cityService.GetAllCitiesAsync());
    }



    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetCityById(Guid id)
    {
        try
        {
            return Ok(await _cityService.GetCityByIdAsync(id));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpPut("edit/{id}")]
    public async Task<IActionResult> UpdateCity(Guid id, [FromBody] UpdateCityDto cityDto)
    {
        try
        {
           
            return Ok (await _cityService.UpdateCityAsync(id, cityDto));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteCity(Guid id)
    {
        try
        {
            await _cityService.DeleteCityAsync(id);
            return Ok("City deleted successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

