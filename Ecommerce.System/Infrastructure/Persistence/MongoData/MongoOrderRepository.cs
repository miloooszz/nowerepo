using Ecommerce.System.Core.Interfaces;
using Ecommerce.System.Core.Models;
using MongoDB.Driver;

namespace Ecommerce.System.Infrastructure.Persistence.MongoData
{
    public class MongoOrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public MongoOrderRepository(MongoDbContext context)
        {
            _orders = context.Orders;
        }

        public async Task<bool> SaveOrderAsync(Order order)
        {
            try
            {
                await _orders.InsertOneAsync(order);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Order>> GetByClientIdAsync(Guid clientId)
        {
            return await _orders.Find(o => o.ClientId == clientId).ToListAsync();
        }

        public async Task<Order> GetOrderDetailsAsync(Guid orderId)
        {
            return await _orders.Find(o => o.Id == orderId).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            // Mongo pobiera cały dokument "Order" 
            return await _orders.Find(_ => true).ToListAsync();
        }
    }
}

