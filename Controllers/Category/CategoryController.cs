using JWT53.Dto.Category;
using JWT53.Services.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT53.Controllers.Category;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("getById/{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDto categoryDto)
    {
        var Newcategory = await _categoryService.AddCategoryAsync(categoryDto);
        return Ok(Newcategory);
    }

    [HttpPut("edit/{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto categoryDto)
    {
 
        await _categoryService.UpdateCategoryAsync(id, categoryDto);
        return Ok("Successfully updated");
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }
}

