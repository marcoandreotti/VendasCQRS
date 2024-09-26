using Domain.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;


[BsonCollection("SalesHistory")]
public class SalesHistoryEntity : Document
{
    [BsonElement("sale-id")]
    public Int64 SaleId { get; set; }

    [BsonElement("sale-date")]
    public DateTime? SaleDate { get; set; }

    [BsonElement("user-name")]
    public string UserName { get; set; }

    [BsonElement("status")]
    public string Status { get; set; }

    [BsonElement("message")]
    public string Message { get; set; }


}
