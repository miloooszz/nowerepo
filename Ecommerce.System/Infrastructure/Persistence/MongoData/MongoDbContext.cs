using MongoDB.Driver;
using Ecommerce.System.Core.Models;

namespace Ecommerce.System.Infrastructure.Persistence.MongoData;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        // Sprawdź czy ConnectionString w appsettings.json nazywa się "MongoDbConnection"
        var connectionString = configuration.GetConnectionString("MongoDbConnection");
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase("EcommerceDb");
    }

    public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
    public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
}