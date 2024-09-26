﻿using Domain.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;

[BsonCollection("Sale")]
public class SaleEntity : Document
{
    [BsonElement("sale-id")] 
    public Int64 SaleId { get; set; }

    [BsonElement("customer")] 
    public CustomerEntity Customer { get; set; }

    [BsonElement("products")] 
    public IEnumerable<ProductEntity> Products { get; set; }

    [BsonElement("sale-date")] 
    public DateTime SaleDate { get; set; }

    [BsonElement("total-sale-price")] 
    public string? TotalSalePrice { get; set; }

    [BsonElement("status")] 
    public int? Status { get; set; }

}
