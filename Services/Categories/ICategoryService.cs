using JWT53.Dto.Category;
using JWT53.Models;

namespace JWT53.Services.Categories;

public interface ICategoryService
{
    Task<IEnumerable<ResponseCategoryDto>> GetAllCategoriesAsync();
    Task<ResponseCategoryDto> GetCategoryByIdAsync(int id);
    Task<Category> AddCategoryAsync(CreateCategoryDto categoryDto);
    Task UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto);
    Task DeleteCategoryAsync(int id);
}
