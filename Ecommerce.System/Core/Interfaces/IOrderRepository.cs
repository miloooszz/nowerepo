using Ecommerce.System.Core.Models;

namespace Ecommerce.System.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<bool> SaveOrderAsync(Order order);

        Task<IEnumerable<Order>> GetByClientIdAsync(Guid clientId);

        Task<Order> GetOrderDetailsAsync(Guid orderId);

        Task<IEnumerable<Order>> GetAllAsync();
    }
}