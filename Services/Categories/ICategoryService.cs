using JWT53.Dto.Category;
using JWT53.Dto.City;
using JWT53.Models;

namespace JWT53.Services.Categories;

public interface ICategoryService
{
   
    Task<IEnumerable<ResponseCategoryDto>> GetAllCategoriesAsync();
    Task<ResponseCategoryDto> GetCategoryByIdAsync(Guid id);
    Task<ResponseCategoryDto>AddCategoryAsync(CreateCategoryDto categoryDto);
    Task UpdateCategoryAsync(Guid id, UpdateCategoryDto categoryDto);
    Task DeleteCategoryAsync(Guid id);
}
