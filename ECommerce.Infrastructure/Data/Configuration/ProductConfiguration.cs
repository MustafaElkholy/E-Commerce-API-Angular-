using Ecommerce.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p=>p.Id).IsRequired();
            builder.Property(p=>p.Name).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal");
            //builder.Property(p => p.PictureURL).IsRequired();   
            builder.HasOne(p=>p.ProductBrand).WithMany().HasForeignKey(p=>p.ProductBrandId);
            builder.HasOne(p=>p.ProductType).WithMany().HasForeignKey(p=>p.ProductTypeId);

        }
    }
}
