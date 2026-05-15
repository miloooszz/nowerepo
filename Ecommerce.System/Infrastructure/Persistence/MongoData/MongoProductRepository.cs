using Ecommerce.System.Core.Interfaces;
using Ecommerce.System.Core.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

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
            return await _products
                .Find(p => p.Name != null && p.Name != "")
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _products
                .Find(p => p.Id == id || p.Variants.Any(v => v.Id == id))
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Product product)
        {
            PrepareProductHierarchy(product);
            await _products.InsertOneAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            PrepareProductHierarchy(product);
            await _products.ReplaceOneAsync(p => p.Id == product.Id, product);
        }

        public async Task BulkInsertAsync(IEnumerable<Product> products)
        {
            if (products != null && products.Any())
            {
                foreach (var product in products)
                {
                    PrepareProductHierarchy(product);
                }
                await _products.InsertManyAsync(products);
            }
        }

        private void PrepareProductHierarchy(Product product)
        {
            if (product.Id == Guid.Empty)
                product.Id = Guid.NewGuid();

            if (product.Variants == null) return;

            foreach (var variant in product.Variants)
            {
                if (variant.Id == Guid.Empty)
                    variant.Id = Guid.NewGuid();

                variant.ProductId = product.Id;

                if (variant.Attributes == null) continue;

                foreach (var attr in variant.Attributes)
                {
                    if (attr.Id == Guid.Empty)
                        attr.Id = Guid.NewGuid();

                    attr.ProductVariantId = variant.Id;
                }
            }
        }
    }
}