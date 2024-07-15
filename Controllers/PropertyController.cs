using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using JWT53.Models;
using Microsoft.AspNetCore.Identity;
using JWT53.Services.Properties;
using JWT53.Dto.Property;
using JWT53.Services.Properties.PropertiesAmenities;
using JWT53.Services.Properties.PropertiesLikes;
namespace JWT53.Controllers.Property;

//[Authorize]
[Route("api/")]
[ApiController]

public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPropertyAmenityService _propertyAmenityService;
    private readonly IPropertyLikeService _propertyLikeService;


    public PropertyController(IPropertyService propertyService, UserManager<ApplicationUser> userManager, IPropertyAmenityService propertyAmenityService, IPropertyLikeService propertyLikeService)
    {
        _propertyService = propertyService;
        _userManager = userManager;
        _propertyAmenityService = propertyAmenityService;
        _propertyLikeService = propertyLikeService;
    }


    [Authorize]
    [HttpGet("Property/getById/{id}")]
    public async Task<ActionResult<ResponsePropertyDto>> GetPropertyById(Guid id)
    {
        var userId = User.FindFirstValue("uid");
        var property = await _propertyService.GetPropertyByIdAsync(id,userId);
        if (property == null)
        {
            return NotFound();
        }
        return Ok(property);
    }


    [Authorize]
    [HttpGet("Property/GetAll")]
    public async Task<ActionResult> GetAllProperties()
    {
        var userId = User.FindFirstValue("uid");
        var properties = await _propertyService.GetAllPropertiesAsync(userId);
        return Ok(properties);
    }



    [HttpPost("Property/add")]
    public async Task<IActionResult> AddProperty([FromBody] CreatePropertyDto createPropertyDto)
    {
        
        await _propertyService.AddPropertyAsync(createPropertyDto, createPropertyDto.UserId);
        return Ok("Successfully Added");
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


    [HttpPost("{propertyId}/addAmenity/{amenityId}")]
    public async Task<IActionResult> AddAmenityToProperty(Guid propertyId, Guid amenityId)
    {
        try
        {
            await _propertyAmenityService.AddAmenityToPropertyAsync(propertyId, amenityId);
            return Ok("Amenity added to property");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("{propertyId}/removeAmenity/{amenityId}")]
    public async Task<IActionResult> RemoveAmenityFromProperty(Guid propertyId, Guid amenityId)
    {
        try
        {
            await _propertyAmenityService.RemoveAmenityFromPropertyAsync(propertyId, amenityId);
            return Ok("Amenity removed from property");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpPost("toggle-like/{propertyId}")]
    public async Task<IActionResult> ToggleLike(Guid propertyId)
    {
        var userId = User.FindFirstValue("uid");

        if (userId == null)
        {
            return Unauthorized();
        }


        try
        {
            // استخدام الخدمة لتبديل حالة الإعجاب والحصول على النتيجة
            var isLiked = await _propertyLikeService.TogglePropertyLikeAsync(userId, propertyId);
            return Ok(isLiked);
        }

        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
