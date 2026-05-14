using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ecommerce.System.Core.Models
{
    [BsonIgnoreExtraElements]
    public class Order
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }

        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        [BsonElement("clientId")]
        public Guid ClientId { get; set; }

        [BsonElement("orderDate")]
        public DateTime OrderDate { get; set; }

        [BsonElement("totalAmount")]
        public decimal TotalAmount { get; set; }

        [BsonElement("status")]
        public string Status { get; set; } = "Pending"; // Obecny status

        // DODAJ TO: Lista wszystkich zmian statusu dla tego zamówienia
        public List<OrderStatus> Statuses { get; set; } = new();

        [BsonElement("items")]
        public List<OrderItem> Items { get; set; } = new();
    }

    public class OrderItem
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        [BsonElement("productId")]
        public Guid ProductId { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("unitPrice")]
        public decimal UnitPrice { get; set; }
    }
}