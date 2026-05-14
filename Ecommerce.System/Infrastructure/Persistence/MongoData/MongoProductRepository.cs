using Ecommerce.System.Core.Interfaces;
using Ecommerce.System.Core.Models;
using MongoDB.Driver;
using System.Linq; 

namespace Ecommerce.System.Infrastructure.Persistence.MongoData
{
    public class MongoProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public MongoProductRepository(MongoDbContext context)
        {
            _products = context.Products;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _products.Find(_ => true).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            // POPRAWKA: Szukamy albo po ID produktu, albo sprawdzamy czy jakikolwiek 
            // wariant wewnątrz listy Variants ma takie ID.
            return await _products
                .Find(p => p.Id == id || p.Variants.Any(v => v.Id == id))
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _products.InsertOneAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            await _products.ReplaceOneAsync(p => p.Id == product.Id, product);
        }

        public async Task BulkInsertAsync(IEnumerable<Product> products)
        {
            if (products != null && products.Any())
            {
                await _products.InsertManyAsync(products);
            }
        }
    }
}