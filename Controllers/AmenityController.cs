using Microsoft.AspNetCore.Mvc;
using JWT53.Services.Amenities;
using JWT53.Dto.Amenity;
using Microsoft.AspNetCore.Authorization;
using JWT53.Services.Citeis;
namespace JWT53.Controllers;
//[Authorize]
[ApiController]
[Route("api/[controller]")]

public class AmenityController : ControllerBase
{
    private readonly IAmenityService _AmenityService;

    public AmenityController(IAmenityService AmenityService)
    {
        _AmenityService = AmenityService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddAmenity([FromBody] CreateAmenityDto AmenityDto)
    {
        return Ok(await _AmenityService.AddAmenityAsync(AmenityDto));
    }



    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllAmenities()
    {

        return Ok(await _AmenityService.GetAllAmenitiesAsync());
    }



    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetAmenityById(Guid id)
    {
        try
        {
            return Ok(await _AmenityService.GetAmenityByIdAsync(id));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpPut("edit/{id}")]
    public async Task<IActionResult> UpdateAmenity(Guid id, [FromBody] UpdateAmenityDto AmenityDto)
    {
        try
        {
            
            return Ok(await _AmenityService.UpdateAmenityAsync(id, AmenityDto));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAmenity(Guid id)
    {
        try
        {
            await _AmenityService.DeleteAmenityAsync(id);
            return Ok("Amenity deleted successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

