using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Classes
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

         public async Task<Product> GetProductByIdAsync(int id)
        {
            return await context.Products.Include(p=>p.ProductBrand).Include(p=>p.ProductType).FirstOrDefaultAsync(p=>p.Id == id);

        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetAllProductTypesAsync()
        {
            return await context.ProductTypes.ToListAsync();
        }

        public async Task<IEnumerable<ProductBrand>> GetAllProductBrandsAsync()
        {
            return await context.ProductBrands.ToListAsync();

        }

    }
}
