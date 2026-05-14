using Ecommerce.System.Core.Interfaces;
using Ecommerce.System.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Ecommerce.System.Infrastructure.Persistence.PostgreSQL
{
    public class SqlOrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public SqlOrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveOrderAsync(Order Order)
        {
            try
            {
                // Entity Framework automatycznie obsłuży to w ramach jednej transakcji bazy danych,
                await _context.Orders.AddAsync(Order);

                // Zapisujemy zmiany w bazie
                var result = await _context.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Order>> GetByClientIdAsync(Guid ClientId)
        {
            // Pobieramy historię zamówień klienta. 
            return await _context.Orders
                .Where(z => z.ClientId == ClientId)
                .OrderByDescending(z => z.DateoftheOrder)
                .ToListAsync();
        }

        public async Task<Order> GetOrderDetailsAsync(Guid orderId)
        {
            // W modelu relacyjnym musimy wykonać operację 'Include' (Join), 
            // aby pobrać pozycje zamówienia razem z nagłówkiem.
            // To będzie jeden z kluczowych punktów Twojej analizy wydajności (SQL Joins vs NoSQL Embedding).
            return await _context.Orders
                .Include(z => _context.OrderStatuses.Where(p => p.  OrderId == orderId))
                .FirstOrDefaultAsync(z => z.Id == orderId);
        }
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            // Pobiera wszystkie zamówienia z bazy
            return await _context.Orders
            .Include(o => o.OrderItems) 
            .ToListAsync();
        }
    }
}