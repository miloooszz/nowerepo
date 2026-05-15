using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Ecommerce.System.Core.Models
{
    [BsonIgnoreExtraElements]
    public class BasketItem
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        [BsonElement("variantId")]
        public Guid VariantId { get; set; }

        [BsonElement("amount")]
        public int Amount { get; set; }
    }
}