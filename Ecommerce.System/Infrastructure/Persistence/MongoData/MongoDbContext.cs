using Ecommerce.System.Core.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.System.Infrastructure.Persistence.MongoData
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDB");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("BŁĄD: Nie znaleziono klucza 'ConnectionStrings:MongoDB' w pliku appsettings.json!");
            }

            if (BsonSerializer.LookupSerializer<GuidSerializer>() == null)
            {
                BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            }

            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);

            _database = client.GetDatabase("EcommerceDb");
        }

        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
        public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
        public IMongoCollection<Client> Clients => _database.GetCollection<Client>("Clients");
    }
}