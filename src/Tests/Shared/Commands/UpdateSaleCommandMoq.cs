using Domain.Contracts;
using Domain.Enums;
using Domain.Features.Commands.UpdateSale;

namespace Tests.Shared.Commands;

public class UpdateSaleCommandMoq
{
    public static UpdateSaleCommand updateSaleSuccessRequest => new UpdateSaleCommand
    {
        CompanyId = 1,
        SaleId = 1,
        SaleDate = DateTime.Now,
        CustomerId = 1,
        Status = SaleStatusEnum.CompraCriada,
        Products = new List<ProductUpdateContract> {
                new ProductUpdateContract {
                    ProductId =1,
                    Quantity = 1,
                    UnitPrice = 1,
                    Discount = 0,
                    Status = SaleItemStatusEnum.ItemCriado,
                }
            }
    };

    public static UpdateSaleCommand updateSaleValidationNotProductRequest => new UpdateSaleCommand
    {
        CompanyId = 1,
        SaleId = 1,
        SaleDate = DateTime.Now,
        CustomerId = 1,
        Status = SaleStatusEnum.CompraCriada,
        Products = new List<ProductUpdateContract> {
                new ProductUpdateContract {
                    UnitPrice = 1,
                    Discount = 0,
                    Status = SaleItemStatusEnum.ItemCriado
                }
            }
    };

}
