using Domain.Contracts;
using Domain.Features.Commands.CreateSale;

namespace Tests.Shared.Commands;

public class CreateSaleCommandMoq
{
    public static CreateSaleCommand createSaleSuccessRequest => new CreateSaleCommand
    {
        SaleId = 1,
        SaleDate = DateTime.Now,
        CustomerId = 1,
        Products = new List<ProductContract> {
                new ProductContract {
                    ProductId =1,
                    Quantity = 1,
                    UnitPrice = 1,
                    Discount = 0
                }
            }
    };

    public static CreateSaleCommand createSaleValidationNotProductRequest => new CreateSaleCommand
    {
        SaleId = 1,
        SaleDate = DateTime.Now,
        CustomerId = 1,
        Products = new List<ProductContract> {
                new ProductContract {
                    UnitPrice = 1,
                    Discount = 0
                }
            }
    };

}
