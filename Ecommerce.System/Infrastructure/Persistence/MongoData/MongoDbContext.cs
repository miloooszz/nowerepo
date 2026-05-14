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
            // 1. Pobranie Connection String (musi być zgodny z kluczem w appsettings.json)
            var connectionString = configuration.GetConnectionString("MongoDB");

            // 2. Walidacja obecności klucza
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("BŁĄD: Nie znaleziono klucza 'ConnectionStrings:MongoDB' w pliku appsettings.json!");
            }

            // 3. Konfiguracja serializacji GUID (rozwiązuje błąd GuidRepresentation Unspecified)
            if (BsonSerializer.LookupSerializer<GuidSerializer>() == null)
            {
                BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            }

            // 4. Inicjalizacja klienta i bazy danych[cite: 1]
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);

            // Pobranie nazwy bazy (domyślnie EcommerceDb)[cite: 1]
            _database = client.GetDatabase("EcommerceDb");
        }

        // Definicje kolekcji[cite: 1]
        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
        public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
        public IMongoCollection<Client> Clients => _database.GetCollection<Client>("Clients");
    }
}