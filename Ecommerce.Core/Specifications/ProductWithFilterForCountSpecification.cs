using Ecommerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Specifications
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecificationsParameters parameters)
           : base(x =>
            (string.IsNullOrEmpty(parameters.Search) || x.Name.ToLower().Contains(parameters.Search)) &&
            (!parameters.BrandId.HasValue || x.ProductBrandId == parameters.BrandId) &&
            (!parameters.TypeId.HasValue || x.ProductTypeId == parameters.TypeId)
            )


        {

        }
    }
}
