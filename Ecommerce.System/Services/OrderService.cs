using Ecommerce.System.Core.Interfaces;
using Ecommerce.System.Core.Models;
using Ecommerce.System.Controllers;

namespace Ecommerce.System.Services
{
    public class OrderService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }
        // Nowa metoda, którą wywoła kontroler
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            // Serwis deleguje zadanie do repozytorium
            return await _orderRepository.GetAllAsync();
        }

        // Zmieniliśmy parametr z List<(Guid, int)> na List<BasketItem>
        public async Task<(bool Success, string Message)> PlaceOrderAsync(Guid clientId, List<BasketItem> basket)
        {
            // 1. Najpierw tworzymy "nagłówek" zamówienia
            var newOrder = new Order
            {
                Id = Guid.NewGuid(),
                ClientId = clientId,
                DateoftheOrder = DateTime.UtcNow,
                Status = "Nowe",
                TotalValue = 0,
                OrderItems = new List<OrderStatus>() // Inicjalizacja listy 📦
            };

            // 2. Teraz pętla, którą edytujemy:
            foreach (var item in basket)
            {
                // Najpierw standardowe sprawdzenia (szukanie produktu i wariantu)
                var product = await _productRepository.GetByIdAsync(item.VariantId);
                if (product == null) return (false, "Nie znaleziono produktu.");

                var variant = product.Variants.FirstOrDefault(w => w.Id == item.VariantId);
                if (variant == null) return (false, "Nie znaleziono wariantu.");

                if (variant.StockStatus < item.Amount) return (false, "Brak towaru.");

                // --- TUTAJ WKLEJASZ TEN FRAGMENT ---
                var detail = new OrderStatus
                {
                    Id = Guid.NewGuid(),
                    OrderId = newOrder.Id,
                    ProductVariantId = item.VariantId,
                    Quantity = item.Amount,
                    Priceatthetimeofpurchase = variant.Price
                };

                newOrder.OrderItems.Add(detail);
                // -----------------------------------

                // Na koniec pętli aktualizujemy stany i ceny
                variant.StockStatus -= item.Amount;
                newOrder.TotalValue += variant.Price * item.Amount;

                await _productRepository.UpdateAsync(product);
            }

            // 3. Zapisujemy wszystko jednym strzałem
            var result = await _orderRepository.SaveOrderAsync(newOrder);
            return result ? (true, "Zamówienie złożone!") : (false, "Błąd zapisu.");
        }

    }
}