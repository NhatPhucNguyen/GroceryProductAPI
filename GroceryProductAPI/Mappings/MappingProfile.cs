using AutoMapper;
using GroceryProductAPI.DTOs;
using GroceryProductAPI.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace GroceryProductAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            //Product mapping
            CreateMap<Product, ProductDTO>().
                ForMember(dest => dest.IngredientsList, 
                opt => opt.MapFrom(p => p.Ingredients.Select(i => i.Name).ToList()))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(p => p.Category.Name));
            CreateMap<ProductForCreationDTO, Product>();
            CreateMap<ProductForUpdateDTO, Product>();
            CreateMap<Product, ProductForUpdateDTO>().
                ForMember(dest => dest.IngredientsList,
                opt => opt.MapFrom(p => p.Ingredients.Select(i => i.Name).ToList()));

            //Category mapping
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryForCreationDTO, Category>();
            CreateMap<CategoryForUpdateDTO, Category>();
            CreateMap<Category,CategoryForUpdateDTO>();
        }
    }
}
