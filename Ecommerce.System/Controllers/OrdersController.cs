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

        // --- NOWA METODA GET ---
        // Pozwala pobrać listę wszystkich zamówień z bazy
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Wywołujemy serwis, aby pobrał dane
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // Dotychczasowy POST do składania zamówień
        [HttpPost("place")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderRequest request)
        {
            var result = await _orderService.PlaceOrderAsync(request.ClientId, request.Basket);

            if (result.Success)
                return Ok(new { Message = result.Message });

            return BadRequest(new { Error = result.Message });
        }
    }

    // Klasy pomocnicze (DTO) - bez zmian
    public class OrderRequest
    {
        public Guid ClientId { get; set; }
        public List<BasketItem> Basket { get; set; }
    }

    public class BasketItem
    {
        public Guid VariantId { get; set; }
        public int Amount { get; set; }
    }
}