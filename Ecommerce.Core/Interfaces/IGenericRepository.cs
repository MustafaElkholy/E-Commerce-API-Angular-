using Ecommerce.Core.Models;
using Ecommerce.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetEntityWithSpecification(ISpecification<T> specification);
        Task<IEnumerable<T>> ListAsync(ISpecification<T> specification);

        Task<int> CountAsync(ISpecification<T> specification);
    }
}
