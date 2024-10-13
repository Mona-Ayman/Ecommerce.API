
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDTO>> Get(string id)
        {
            var basket = await serviceManager.BasketService.GetBasketAsync(id);
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDTO>> Update(BasketDTO basketDTO)
        {
           var basket = await serviceManager.BasketService.UpdateBasketAsync(basketDTO);
            return Ok(basket);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }


    }
}
