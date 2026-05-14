using Ecommerce.System.Core.Models;

namespace Ecommerce.System.Core.Interfaces
{
    public interface IOrderRepository
    {
        // Zapisuje gotowe zamówienie
        Task<bool> SaveOrderAsync(Order order);

        // Pobiera zamówienia konkretnego klienta
        Task<IEnumerable<Order>> GetByClientIdAsync(Guid clientId);

        // Pobiera pełne dane o zamówieniu
        Task<Order> GetOrderDetailsAsync(Guid orderId);

        Task<IEnumerable<Order>> GetAllAsync();
    }
}