using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public ProductType? ProductType { get; set; }
        [ForeignKey("ProductType")]
        public int? ProductTypeId { get; set; }
        public ProductBrand? ProductBrand { get; set; }
        [ForeignKey("ProductBrand")]
        public int? ProductBrandId { get; set; }

    }
}
