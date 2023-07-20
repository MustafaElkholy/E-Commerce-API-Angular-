using AutoMapper;
using E_Commerce.API.DTOs;
using Ecommerce.Core.Models;

namespace E_Commerce.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, GetProductDTO>().
                ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductURLResolver>());
                

        }
    }
}
