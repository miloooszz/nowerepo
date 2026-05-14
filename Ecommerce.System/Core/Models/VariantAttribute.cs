using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ecommerce.System.Core.Models
{
    [BsonIgnoreExtraElements]
    public class VariantAttribute
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }

        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        [BsonElement("productVariantId")]
        public Guid ProductVariantId { get; set; }

        [BsonElement("key")]
        public string Key { get; set; } = string.Empty;

        [BsonElement("value")]
        public string Value { get; set; } = string.Empty;
    }
}