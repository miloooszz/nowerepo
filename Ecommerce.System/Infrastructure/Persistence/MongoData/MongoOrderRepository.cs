using Ecommerce.System.Core.Interfaces;
using Ecommerce.System.Core.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                if (order.Id == Guid.Empty) order.Id = Guid.NewGuid();
                if (order.OrderDate == default) order.OrderDate = DateTime.UtcNow;

                await _orders.InsertOneAsync(order);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // NAPRAWA: Zmiana typu zwracanego z List na IEnumerable, aby pasował do interfejsu
        public async Task<IEnumerable<Order>> GetByClientIdAsync(Guid clientId)
        {
            return await _orders.Find(o => o.ClientId == clientId).ToListAsync();
        }

        // NAPRAWA: Zmiana typu zwracanego z List na IEnumerable, aby pasował do interfejsu
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _orders.Find(_ => true).ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _orders.Find(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Order?> GetOrderDetailsAsync(Guid orderId)
        {
            return await _orders.Find(o => o.Id == orderId).FirstOrDefaultAsync();
        }
    }
}