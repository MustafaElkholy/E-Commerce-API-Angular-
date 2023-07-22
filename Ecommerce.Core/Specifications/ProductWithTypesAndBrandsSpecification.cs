using Ecommerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Specifications
{
    public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductWithTypesAndBrandsSpecification(ProductSpecificationsParameters parameters)
       : base(x =>
            (string.IsNullOrEmpty(parameters.Search) || x.Name.ToLower().Contains(parameters.Search)) &&
            (!parameters.BrandId.HasValue || x.ProductBrandId == parameters.BrandId) &&
            (!parameters.TypeId.HasValue || x.ProductTypeId == parameters.TypeId)
            )



        {
            AddIncludes(x => x.ProductType);
            AddIncludes(x => x.ProductBrand);

            AddOrderBy(x => x.Id);
            ApplyPagination(parameters.PageSize * (parameters.Page - 1), parameters.PageSize);


            if (!string.IsNullOrEmpty(parameters.Sort))
            {
                switch (parameters.Sort)
                {
                    case "name":
                        AddOrderBy(x => x.Name);
                        break;

                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;

                    default:
                        AddOrderBy(x => x.Id);
                        break;
                }
            }
        }
        public ProductWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddIncludes(x => x.ProductType);
            AddIncludes(x => x.ProductBrand);
        }

    }
}
