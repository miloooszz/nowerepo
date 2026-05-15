using Ecommerce.System.Core.Interfaces;
using Ecommerce.System.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.System.Infrastructure.Persistence.PostgreSQL
{
    public class SqlOrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public SqlOrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveOrderAsync(Order order)
        {
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByClientIdAsync(Guid clientId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .Where(o => o.ClientId == clientId)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order?> GetOrderDetailsAsync(Guid orderId)
        {
            return await GetByIdAsync(orderId);
        }
    }
}