using Domain.Contracts;
using Domain.Features.Commands.CreateBuy;

namespace Tests.Shared.Commands;

public class CreateBuyCommandMoq
{
    public static CreateBuyCommand createBuySuccessRequest => new CreateBuyCommand
    {
        BuyId = 1,
        BuyDate = DateTime.Now,
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

    public static CreateBuyCommand createBuyValidationNotProductRequest => new CreateBuyCommand
    {
        BuyId = 1,
        BuyDate = DateTime.Now,
        CustomerId = 1,
        Products = new List<ProductContract> {
                new ProductContract {
                    UnitPrice = 1,
                    Discount = 0
                }
            }
    };

}
