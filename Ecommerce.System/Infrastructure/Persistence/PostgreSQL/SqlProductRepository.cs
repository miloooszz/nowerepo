using Ecommerce.System.Core.Interfaces;
using Ecommerce.System.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.System.Infrastructure.Persistence.PostgreSQL
{
    public class SqlProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public SqlProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Variants)
                .ThenInclude(w => w.Attributes)
                .FirstOrDefaultAsync(p => p.Variants.Any(w => w.Id == id));
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Variants)
                .ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task BulkInsertAsync(IEnumerable<Product> products)
        {
            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}