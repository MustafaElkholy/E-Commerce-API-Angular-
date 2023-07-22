using AutoMapper;
using E_Commerce.API.DTOs;
using E_Commerce.API.Helpers;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using Ecommerce.Core.Specifications;
using ECommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //private readonly ApplicationDbContext context;
        //private readonly IProductRepository productRepo;

        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<ProductBrand> brandRepo;
        private readonly IGenericRepository<ProductType> typeRepo;
        private readonly IMapper mapper;

        public ProductController
            (IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> brandRepo, 
            IGenericRepository<ProductType> typeRepo,
            IMapper mapper)
        {
            this.productRepo = productRepo;
            this.brandRepo = brandRepo;
            this.typeRepo = typeRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pagination<GetProductDTO>>> GetAllProducts([FromQuery]ProductSpecificationsParameters parameters)
        {
            var productSpecification = new ProductWithTypesAndBrandsSpecification(parameters);
            var countSpecification = new ProductWithFilterForCountSpecification(parameters);

            var totalItems = await productRepo.CountAsync(countSpecification);

            var products = await productRepo.ListAsync(productSpecification);
            var data = mapper.Map<IEnumerable<GetProductDTO>>(products);

            return Ok(new Pagination<GetProductDTO>(parameters.Page,parameters.PageSize,totalItems,data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var productSpecification = new ProductWithTypesAndBrandsSpecification(id);

            var product = await productRepo.GetEntityWithSpecification(productSpecification);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<GetProductDTO>(product));
        }


        [HttpGet("brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetAllProductBrands()
        {
            var brands = await brandRepo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetAllProductTypes()
        {
            var types = await typeRepo.GetAllAsync();
            return Ok(types);
        }




    }
}
