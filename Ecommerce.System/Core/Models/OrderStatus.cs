using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ecommerce.System.Core.Models
{
    [BsonIgnoreExtraElements] 
    public class OrderStatus
    {
        [BsonId] 
        [BsonGuidRepresentation(GuidRepresentation.Standard)] 
        public Guid Id { get; set; }

        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        [BsonElement("orderId")] 
        public Guid OrderId { get; set; }

        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        [BsonElement("productVariantId")]
        public Guid ProductVariantId { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("priceAtPurchase")]
        public decimal Priceatthetimeofpurchase { get; set; }

        [BsonElement("changedAt")]
        public DateTime ChangedAt { get; set; }

        [BsonElement("status")]
        public string Status { get; set; } = string.Empty;
    }
}