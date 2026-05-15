using Ecommerce.System.Core.Interfaces;
using Ecommerce.System.Core.Models;
using Ecommerce.System.Infrastructure.Persistence.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.System.Infrastructure.Persistence.PostgreSQL
{
    public class SqlClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public SqlClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(Guid id)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Client client)
        {
            if (client.Id == Guid.Empty)
            {
                client.Id = Guid.NewGuid();
            }
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }
    }
}