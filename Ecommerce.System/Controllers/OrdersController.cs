using Ecommerce.System.Core.Models;
using Ecommerce.System.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpPost("place")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderRequest request)
        {
            if (request.Basket == null || !request.Basket.Any())
            {
                return BadRequest(new { Error = "Koszyk jest pusty." });
            }

            var result = await _orderService.PlaceOrderAsync(request.ClientId, request.Basket);

            if (result.Success)
                return Ok(new { Message = result.Message });

            return BadRequest(new { Error = result.Message });
        }
    }

    public class OrderRequest
    {
        public Guid ClientId { get; set; }
        public List<BasketItem> Basket { get; set; } = new();
    }
}