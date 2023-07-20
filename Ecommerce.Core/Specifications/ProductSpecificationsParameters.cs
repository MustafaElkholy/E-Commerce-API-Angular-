using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Specifications
{
    public class ProductSpecificationsParameters
    {
        private int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int pageSize = 6;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value>MaxPageSize)? MaxPageSize : value;
        }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }

    }
}
