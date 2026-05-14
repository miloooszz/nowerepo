using Ecommerce.System.Core.Interfaces;
using Ecommerce.System.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Metoda do dodawania produktów 
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product.Id == Guid.Empty) product.Id = Guid.NewGuid();

            if (product.Variants != null)
            {
                foreach (var w in product.Variants)
                {
                    if (w.Id == Guid.Empty) w.Id = Guid.NewGuid();
                    w.ProductId = product.Id;
                }
            }

            await _productRepository.AddAsync(product);
            return Ok(product);
        }

        // Metoda do pobierania wszystkich produktów
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

    }
}