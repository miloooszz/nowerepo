using Ecommerce.System.Core.Models;

namespace Ecommerce.System.Core.Interfaces
{
    public interface IProductRepository
    {
        // Pobiera produkt wraz z wariantami
        Task<Product> GetByIdAsync(Guid id);

        // Pobiera listę wszystkich produktów
        Task<IEnumerable<Product>> GetAllAsync();

        // Dodaje pojedynczy produkt
        Task AddAsync(Product product);

        // Metoda do masowego zasilania bazy (testy obciążeniowe)
        Task BulkInsertAsync(IEnumerable<Product> products);
        Task UpdateAsync(Product product);
    }
}