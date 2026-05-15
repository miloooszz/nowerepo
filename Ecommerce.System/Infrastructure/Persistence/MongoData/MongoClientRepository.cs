using Ecommerce.System.Core.Interfaces;
using Ecommerce.System.Core.Models;
using MongoDB.Driver;

namespace Ecommerce.System.Infrastructure.Persistence.MongoData
{
    public class MongoClientRepository : IClientRepository
    {
        private readonly IMongoCollection<Client> _clients;

        public MongoClientRepository(MongoDbContext context)
        {
            _clients = context.Clients;
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _clients.Find(_ => true).ToListAsync();
        }

        public async Task<Client> GetByIdAsync(Guid id)
        {
            return await _clients.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Client client)
        {
            if (client.Id == Guid.Empty)
            {
                client.Id = Guid.NewGuid();
            }
            await _clients.InsertOneAsync(client);
        }
    }
}