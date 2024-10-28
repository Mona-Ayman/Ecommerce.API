
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace Presentation
{

    public class BasketController(IServiceManager serviceManager) : ApiController
    {
        //[HttpGet("{id}")]    // the angular project send the id as a query string not segment so i commented that
        [HttpGet]
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
