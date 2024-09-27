using Domain.Entities;
using MongoDB.Bson;

namespace Tests.Shared.Repository;

public class BuyEntityMoq
{
    public static BuyEntity BuyEntityResponse => new BuyEntity
    {
        Id = ObjectId.GenerateNewId(),
        BuyDate = DateTime.Now,
        BuyId = 1,
        Customer = new CustomerEntity { CustomerId = 1, Name = "Teste 1" },
        Status = 1,
        TotalBuyPrice = 1,
        Products = new List<ProductEntity> {
            new ProductEntity {
                ProductId = 1,
                Name = "Produto Teste 1",
                Quantity = 1,
                UnitPrice = 1,
                Discount = 0,
                Status = 1
            }
        }
    };
}
