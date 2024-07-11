using JWT53.Dto.Category;
using JWT53.Dto.Category;
using JWT53.Services.Categories;
using JWT53.Services.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT53.Controllers.Category;
//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _CategoryService;

    public CategoryController(ICategoryService CategoryService)
    {
        _CategoryService = CategoryService;
    }

    [HttpPost("/add")]
    public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDto CategoryDto)
    {
        return Ok(await _CategoryService.AddCategoryAsync(CategoryDto));
    }


    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllCategories()
    {
        return Ok(await _CategoryService.GetAllCategoriesAsync());
    }



    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        try
        { 
            return Ok(await _CategoryService.GetCategoryByIdAsync(id));
        }

        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpPut("edit/{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryDto CategoryDto)
    {
        try
        {
            await _CategoryService.UpdateCategoryAsync(id, CategoryDto);
            return Ok("Category updated successfully");
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        try
        {
            await _CategoryService.DeleteCategoryAsync(id);
            return Ok("Category deleted successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

