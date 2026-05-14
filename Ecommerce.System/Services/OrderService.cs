using Ecommerce.System.Core.Interfaces;
using Ecommerce.System.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<(bool Success, string Message)> PlaceOrderAsync(Guid clientId, List<BasketItem> basket)
        {
            if (basket == null || !basket.Any())
                return (false, "Koszyk jest pusty.");

            // 1. Inicjalizacja nagłówka zamówienia[cite: 1]
            var newOrder = new Order
            {
                Id = Guid.NewGuid(),
                ClientId = clientId,
                OrderDate = DateTime.UtcNow,
                Status = "New",
                TotalAmount = 0,
                Items = new List<OrderItem>()
            };

            var processedProducts = new List<Product>();

            // 2. Przetwarzanie koszyka i walidacja[cite: 1]
            foreach (var item in basket)
            {
                // Pobranie produktu zawierającego dany wariant
                var product = await _productRepository.GetByIdAsync(item.VariantId);
                if (product == null)
                    return (false, $"Nie znaleziono produktu dla wariantu: {item.VariantId}");

                var variant = product.Variants.FirstOrDefault(v => v.Id == item.VariantId);
                if (variant == null)
                    return (false, "Błąd wewnętrzny: Nie znaleziono wariantu w produkcie.");

                // Sprawdzenie dostępności towaru[cite: 1]
                if (variant.StockStatus < item.Amount)
                    return (false, $"Brak towaru dla: {product.Name}. Dostępne: {variant.StockStatus}, żądano: {item.Amount}");

                // Tworzenie pozycji zamówienia[cite: 1]
                var orderItem = new OrderItem
                {
                    ProductId = variant.Id,
                    Quantity = item.Amount,
                    UnitPrice = variant.Price
                };

                newOrder.Items.Add(orderItem);
                newOrder.TotalAmount += (variant.Price * item.Amount);

                // Aktualizacja stanu magazynowego w pamięci[cite: 1]
                variant.StockStatus -= item.Amount;
                processedProducts.Add(product);
            }

            // 3. Zapis zmian w produktach (aktualizacja magazynu)[cite: 1]
            foreach (var prod in processedProducts)
            {
                await _productRepository.UpdateAsync(prod);
            }

            // 4. Finalny zapis zamówienia do bazy danych[cite: 1]
            var result = await _orderRepository.SaveOrderAsync(newOrder);

            if (!result)
            {
                // W prawdziwym systemie tutaj należałoby przywrócić stany magazynowe (Rollback)
                return (false, "Wystąpił krytyczny błąd podczas zapisywania zamówienia w bazie.");
            }

            return (true, "Zamówienie złożone pomyślnie!");
        }
    }
}