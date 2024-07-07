using JWT53.Data;
using JWT53.Dto.Category;
using JWT53.Dto.Property;
using JWT53.Models;
using Microsoft.EntityFrameworkCore;

namespace JWT53.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }




    public async Task<IEnumerable<ResponseCategoryDto>> GetAllCategoriesAsync()
    {
        return await _context.Categories
            .Select(category => new ResponseCategoryDto
            {
                Id = category.Id,
                Name_Ar = category.CategoryName_Ar,
                Name_En = category.CategoryName_En,
                Name_Ku = category.CategoryName_Ku,
                CategoryIconUrl = category.CategoryIconUrl,
                //Properties = category.Properties.Select(p => new PropertyDto
                //{
                //    Id = p.Id,
                //    Name_Ar = p.Name_Ar,
                //    Name_En = p.Name_En,
                //    Name_Ku = p.Name_Ku,
                //}).ToList()
            })
            .ToListAsync();
    }



    public async Task<Category> AddCategoryAsync(CreateCategoryDto categoryDto)
    {
        var category = new Category
        {
            CategoryName_Ar = categoryDto.Name_Ar,
            CategoryName_En = categoryDto.Name_En,
            CategoryName_Ku = categoryDto.Name_Ku,
            CategoryIconUrl = "/Soon/Soon"
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return category;
    }


    public async Task<ResponseCategoryDto> GetCategoryByIdAsync(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Properties)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return null;
        }

        return new ResponseCategoryDto
        {
            Id = category.Id,
            Name_Ar = category.CategoryName_Ar,
            Name_En = category.CategoryName_En,
            Name_Ku = category.CategoryName_Ku,
            CategoryIconUrl = category.CategoryIconUrl,
            //Properties = category.Properties.Select(p => new PropertyDto
            //{
            //    Id = p.Id,
            //    Name_Ar = p.Name_Ar,
            //    Name_En = p.Name_En,
            //    Name_Ku = p.Name_Ku,
            //}).ToList()
        };
    }


    public async Task UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            throw new Exception("Category not found");
        }

        category.CategoryName_Ar = categoryDto.Name_Ar;
        category.CategoryName_En = categoryDto.Name_En;
        category.CategoryName_Ku = categoryDto.Name_Ku;
       // category.CategoryIconUrl = categoryDto.CategoryIconUrl;

        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}

