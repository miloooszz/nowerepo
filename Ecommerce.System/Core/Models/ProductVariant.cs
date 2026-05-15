using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ecommerce.System.Core.Models
{
    [BsonIgnoreExtraElements] 
    public class ProductVariant
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }

        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid ProductId { get; set; }

        [BsonElement("ean")]
        public string EAN { get; set; } = string.Empty;

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("stockStatus")]
        public int StockStatus { get; set; }

        [BsonElement("attributes")]
        public List<VariantAttribute> Attributes { get; set; } = new();
    }
}