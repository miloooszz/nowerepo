using Ecommerce.System.Core.Models;

namespace Ecommerce.System.Core.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllAsync();
        Task<Client> GetByIdAsync(Guid id);
        Task AddAsync(Client client);
    }
}