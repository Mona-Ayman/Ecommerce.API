
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.ErrorModels;
using System;
using System.Net;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResultDTO>>> GetProducts([FromQuery] ProductSpecificationParameters parameters)
        {
            var products = await serviceManager.ProductService.GetAllProductsAsync(parameters);
            return Ok(products);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDTO>>> GetTypes()
        {
            var types = await serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDTO>>> GetBrands()
        {
            var brands = await serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProductResultDTO), (int)HttpStatusCode.OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResultDTO>> GetProduct(int id)
        {
            var product = await serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }

    }
}
