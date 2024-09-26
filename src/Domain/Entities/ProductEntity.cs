using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ProductEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int64)]
        [BsonElement("product-id")]
        public Int64 ProductId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("quantity")]
        public decimal Quantity { get; set; }

        [BsonElement("unit-price")]
        public decimal UnitPrice { get; set; }

        [BsonElement("discount")]
        public decimal Discount { get; set; }

        [BsonElement("total-price")]
        public decimal? TotalPrice { get; set; }

        [BsonElement("status")]
        public string? Status { get; set; }
    }
}
