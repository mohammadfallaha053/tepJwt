using JWT53.Services.Seller.Property;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using JWT53.Models;
using Microsoft.AspNetCore.Identity;
namespace JWT53.Controllers.Propertys;


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

    [Authorize(Roles = "Seller")]
    [HttpGet("seller/Property/{id}")]
    public async Task<ActionResult<Property>> GetPropertyById(int id)
    {
        var property = await _propertyService.GetPropertyByIdAsync(id);
        if (property == null)
        {
            return NotFound();
        }
        return Ok(property);
    }

    [Authorize(Roles = "admin")]
    [HttpGet("admin/Property")]
    public async Task<ActionResult<IEnumerable<Property>>> GetAllProperties()
    {
        var properties = await _propertyService.GetAllPropertiesAsync();
        return Ok(properties);
    }


    [Authorize(Roles = "admin")]
    [HttpGet("seller/Property")]
    public async Task<ActionResult<IEnumerable<Property>>> GetsellerProperties()
    {
        var properties = await _propertyService.GetAllPropertiesAsync();
        return Ok(properties);
    }


    [Authorize(Roles = "seller")]
    [HttpPost("seller/Property")]
    public async Task<IActionResult> AddProperty(Property property)
    {
        var userId = _userManager.GetUserId(User);
        await _propertyService.AddPropertyAsync(property, userId!);
        return CreatedAtAction(nameof(GetPropertyById), new { id = property.Id }, property);
    }

    [Authorize(Roles = "seller")]
    [HttpPut("seller/Property/{id}")]
    public async Task<IActionResult> UpdateProperty(int id, Property property)
    {
        if (id != property.Id)
        {
            return BadRequest();
        }
        await _propertyService.UpdatePropertyAsync(property);
        return NoContent();
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("admin/Property/{id}")]
    public async Task<IActionResult> DeleteProperty(int id)
    {
        await _propertyService.DeletePropertyAsync(id);
        return NoContent();
    }
}
