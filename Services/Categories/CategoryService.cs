using AutoMapper;
using JWT53.Data;
using JWT53.Dto.Category;
using JWT53.Dto.Category;
using JWT53.Dto.Property;
using JWT53.Models;
using JWT53.Services.Categories;
using Microsoft.EntityFrameworkCore;

namespace JWT53.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CategoryService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseCategoryDto> AddCategoryAsync(CreateCategoryDto CategoryDto)
    {

       
        var Category = _mapper.Map<Category>(CategoryDto);
        Category.Id = Guid.NewGuid();
        Category.ImageUrl = "Soon/Soon";
        _context.Categories.Add(Category);
        await _context.SaveChangesAsync();


        return _mapper.Map<ResponseCategoryDto>(Category);

    }

    public async Task<IEnumerable<ResponseCategoryDto>> GetAllCategoriesAsync()
    {
        var Categories = await _context.Categories.ToListAsync();
        return _mapper.Map<IEnumerable<ResponseCategoryDto>>(Categories);
    }


    public async Task<ResponseCategoryDto> GetCategoryByIdAsync(Guid id)
    {
        var Category = await _context.Categories.FindAsync(id);
        if (Category == null)
        {
            throw new Exception("Category not found");
        }
        return _mapper.Map<ResponseCategoryDto>(Category);
    }


    public async Task UpdateCategoryAsync(Guid id, UpdateCategoryDto CategoryDto)
    {
        var Category = await _context.Categories.FindAsync(id);
        if (Category == null)
        {
            throw new Exception("Category not found");
        }

        _mapper.Map(CategoryDto, Category);
        _context.Categories.Update(Category);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteCategoryAsync(Guid id)
    {
        var Category = await _context.Categories.Include(c => c.Properties).FirstOrDefaultAsync(c => c.Id == id);
        if (Category == null)
        {
            throw new Exception("Category not found");
        }

        if (Category.Properties.Any())
        {
            throw new Exception("Cannot delete Category with associated properties");
        }

        _context.Categories.Remove(Category);
        await _context.SaveChangesAsync();
    }
}

