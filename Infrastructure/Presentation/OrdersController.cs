
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.OrderModels;
using System.Security.Claims;

namespace Presentation
{
    [Authorize]
    public class OrdersController(IServiceManager serviceManager) : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<OrderResultDTO>> CreateOrder(OrderRequestDTO request)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await serviceManager.OrderService.CreateOrderAsync(request, email);
            return Ok(order);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderRequestDTO>> GetOrderById(Guid id)
        {
            var order = await serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResultDTO>>> GetOrdersByEmail()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await serviceManager.OrderService.GetOrdersByEmaillAsync(email);
            return Ok(orders);
        }
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDTO>>> GetDeliveryMethods()
        {
       
            var methods = await serviceManager.OrderService.GetDeliveryMethodAsync();
            return Ok(methods);
        }
    }
}
