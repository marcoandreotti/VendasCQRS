using Domain.Contracts;
using Domain.Enums;
using Domain.Features.Commands.UpdateBuy;

namespace Tests.Shared.Commands;

public class UpdateBuyCommandMoq
{
    public static UpdateBuyCommand updateBuySuccessRequest => new UpdateBuyCommand
    {
        BuyId = 1,
        BuyDate = DateTime.Now,
        CustomerId = 1,
        Status = BuyStatusEnum.CompraCriada,
        Products = new List<ProductUpdateContract> {
                new ProductUpdateContract {
                    ProductId =1,
                    Quantity = 1,
                    UnitPrice = 1,
                    Discount = 0,
                    Status = BuyItemStatusEnum.ItemCriado,
                }
            }
    };

    public static UpdateBuyCommand updateBuyValidationNotProductRequest => new UpdateBuyCommand
    {
        BuyId = 1,
        BuyDate = DateTime.Now,
        CustomerId = 1,
        Status = BuyStatusEnum.CompraCriada,
        Products = new List<ProductUpdateContract> {
                new ProductUpdateContract {
                    UnitPrice = 1,
                    Discount = 0,
                    Status = BuyItemStatusEnum.ItemCriado
                }
            }
    };

}
