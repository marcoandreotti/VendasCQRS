using Domain.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;


[BsonCollection("BuyHistory")]
public class BuyHistoryEntity : Document
{
    [BsonElement("buy-id")]
    public Int64 BuyId { get; set; }

    [BsonElement("buy-date")]
    public DateTime? BuyDate { get; set; }

    [BsonElement("user-name")]
    public string UserName { get; set; }

    [BsonElement("status")]
    public string Status { get; set; }

    [BsonElement("message")]
    public string Message { get; set; }


}
