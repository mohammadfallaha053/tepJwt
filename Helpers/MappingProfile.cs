using AutoMapper;
using JWT53.Dto.Category;
using JWT53.Dto.City;
using JWT53.Models;

namespace JWT53.Helpers;

public class MappingProfile:Profile
{

    public MappingProfile()
    {
        CreateMap<CreateCityDto, City>();
        CreateMap<City,ResponseCityDto>();
        CreateMap<UpdateCityDto, City>();


        CreateMap<CreateCategoryDto, Category>();
        CreateMap<Category, ResponseCategoryDto>();
        CreateMap<UpdateCategoryDto, Category>();

    }
}
