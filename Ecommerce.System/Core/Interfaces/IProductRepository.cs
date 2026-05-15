using Ecommerce.System.Core.Models;

namespace Ecommerce.System.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id);

        Task<IEnumerable<Product>> GetAllAsync();

        Task AddAsync(Product product);

        Task BulkInsertAsync(IEnumerable<Product> products);
        Task UpdateAsync(Product product);
    }
}