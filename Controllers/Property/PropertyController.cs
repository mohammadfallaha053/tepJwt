using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using JWT53.Models;
using Microsoft.AspNetCore.Identity;
using JWT53.Services.Property;
using JWT53.Dto.Property;
namespace JWT53.Controllers.Property;

//[Authorize]
[Route("api/")]
[ApiController]

public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly UserManager<ApplicationUser> _userManager;

    public PropertyController(IPropertyService propertyService, UserManager<ApplicationUser> userManager)
    {
        _propertyService = propertyService;
        _userManager = userManager;
    }

   

    [HttpGet("Property/getById/{id}")]
    public async Task<ActionResult<ResponsePropertyDto>> GetPropertyById(Guid id)
    {
        var property = await _propertyService.GetPropertyByIdAsync(id);
        if (property == null)
        {
            return NotFound();
        }
        return Ok(property);
    }


   
    [HttpGet("Property/GetAll")]
    public async Task<ActionResult> GetAllProperties()
    {
        var properties = await _propertyService.GetAllPropertiesAsync();
        return Ok(properties);
    }



    [HttpPost("Property/add")]
    public async Task<IActionResult> AddProperty([FromBody] CreatePropertyDto createPropertyDto)
    {
        
        await _propertyService.AddPropertyAsync(createPropertyDto, createPropertyDto.UserId);
        return Ok();
    }


    [HttpPut("edit/{id}")]
    public async Task<IActionResult> UpdateProperty(Guid id, [FromBody] UpdatePropertyDto dto)
    {
        try
        {
            await _propertyService.UpdatePropertyAsync(id, dto);
            return Ok("Successfully updated");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("Property/delete/{id}")]
    public async Task<IActionResult> DeleteProperty(Guid id)
    {
        await _propertyService.DeletePropertyAsync(id);
        return NoContent();
    }

}
