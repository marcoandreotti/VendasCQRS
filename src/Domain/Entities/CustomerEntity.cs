using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;

public class CustomerEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.Int64)]
    [BsonElement("customer-id")]
    public Int64 CustomerId { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }
}
