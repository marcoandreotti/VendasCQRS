using Domain.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;

[BsonCollection("Buy")]
public class BuyEntity : Document
{
    [BsonElement("buy-id")]
    public Int64 BuyId { get; set; }

    [BsonElement("customer")]
    public CustomerEntity Customer { get; set; }

    [BsonElement("products")]
    public IEnumerable<ProductEntity> Products { get; set; }

    [BsonElement("buy-date")]
    public DateTime BuyDate { get; set; }

    [BsonElement("total-buy-price")]
    public decimal TotalBuyPrice { get; set; }

    [BsonElement("status")]
    public int Status { get; set; }

}
