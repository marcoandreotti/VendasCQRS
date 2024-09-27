using Domain.Entities;

namespace Tests.Shared.Queries;

public class SaleEntityResponseMoq
{
    public static List<SaleEntity> listEntity =>
    [
        new SaleEntity
        {
            SaleId = 1,
            SaleDate = DateTime.Now,
            Status = 1,
            TotalSalePrice = 10,
            Customer = new CustomerEntity
            {
                CustomerId = 1,
                Name = "Cliente Test 1"
            },
            Products = new List<ProductEntity>
            {
                new ProductEntity
                {
                    ProductId = 1,
                    Name = "Produto Teste 1",
                    Quantity = 1,
                    UnitPrice = 10,
                    Discount = 0,
                    Status = 1
                }
            }
        }
    ];

    public static SaleEntity entity => new()
    {
        SaleId = 1,
        SaleDate = DateTime.Now,
        Status = 1,
        TotalSalePrice = 10,
        Customer = new CustomerEntity
        {
            CustomerId = 1,
            Name = "Cliente Test 1"
        },
        Products = new List<ProductEntity>
        {
            new ProductEntity
            {
                ProductId = 1,
                Name = "Produto Teste 1",
                Quantity = 1,
                UnitPrice = 10,
                Discount = 0,
                Status = 1
            }
        }
    };
}