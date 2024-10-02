
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("[controller]/api")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResultDTO>>> GetProducts()
        {
            var products = await serviceManager.ProductService.GetAllProductsAsync();
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResultDTO>> GetProduct(int id)
        {
            var product = await serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }

    }
}
