using Ecommerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Data
{
    public class StoreContextSeeding
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            

            if (!context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("E:\\My Data\\ITI .Net Track\\ASP.NET Core Web API\\SelfLearning\\E-Commerce\\E-CommerceApplication\\ECommerce.Infrastructure\\Data\\DataSeeding\\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
               await context.ProductBrands.AddRangeAsync(brands);
            }

            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("E:\\My Data\\ITI .Net Track\\ASP.NET Core Web API\\SelfLearning\\E-Commerce\\E-CommerceApplication\\ECommerce.Infrastructure\\Data\\DataSeeding\\types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
               await context.ProductTypes.AddRangeAsync(types);
            }
            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("E:\\My Data\\ITI .Net Track\\ASP.NET Core Web API\\SelfLearning\\E-Commerce\\E-CommerceApplication\\ECommerce.Infrastructure\\Data\\DataSeeding\\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                await context.Products.AddRangeAsync(products);
            }

            if (context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }
            
        }
    }
}
