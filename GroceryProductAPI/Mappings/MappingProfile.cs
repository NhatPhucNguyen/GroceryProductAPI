using AutoMapper;
using GroceryProductAPI.DTOs;
using GroceryProductAPI.Models;

namespace GroceryProductAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Product, ProductDTO>().ForMember(dest => dest.Ingredients, opt => opt.MapFrom(p => p.Ingredients.Select(i => i.Name).ToList()));
        }
    }
}
