using AutoMapper;
using E_Commerce.API.DTOs;
using Ecommerce.Core.Models;

namespace E_Commerce.API.Helpers
{
    public class ProductURLResolver : IValueResolver<Product, GetProductDTO, string>
    {
        private readonly IConfiguration configuration;

        public ProductURLResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Resolve(Product source, GetProductDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return configuration["APIUrl"] + source.PictureUrl;

            }
            return null;
        }
    }
}
