using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using Ecommerce.Core.Specifications;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Classes
{
    public class GenericRepository<T> : IGenericRepository<T> where T: BaseEntity
    {
        private readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CountAsync(ISpecification<T> specification)
        {
            return await ApplySpecifications(specification).CountAsync();   
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await context.Set<T>().FindAsync(id);

        public async Task<T> GetEntityWithSpecification(ISpecification<T> specification)
        {
            return await ApplySpecifications(specification).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> ListAsync(ISpecification<T> specification)
        {
            return await ApplySpecifications(specification).ToListAsync();
        }


        private IQueryable<T> ApplySpecifications(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(),specification);
        }
    }
}
