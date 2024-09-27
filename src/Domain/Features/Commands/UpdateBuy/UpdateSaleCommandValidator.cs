using FluentValidation;

namespace Domain.Features.Commands.UpdateSale;

public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleCommandValidator()
    {
        RuleFor(r => r.CompanyId)
            .NotNull()
            .NotEqual(0)
            .WithMessage("Id da Filial é requerido.")
            .GreaterThan(0)
            .WithMessage("Id da Filial - não deve ser menor que 0");

        RuleFor(r => r.SaleId)
             .NotNull()
             .NotEqual(0)
             .WithMessage("Id da Compra é requerido.")
             .GreaterThan(0)
             .WithMessage("Id da Compra - não deve ser menor que 0");

        RuleFor(x => x.CustomerId)
            .NotNull()
            .WithMessage("Id do Cliente é requerido.")
            .GreaterThan(0)
            .WithMessage("Id do Cliente - não deve ser menor que 0");

        RuleFor(r => r.Products)
            .NotNull()
            .WithMessage("Produto é requerido.");

        RuleFor(x => x.Products.Select(p => p.ProductId))
            .Must(x => !x.Any(s => s <= 0))
            .WithMessage("Id do Product é requerido.");

        RuleFor(x => x.Products.Select(p => p.Quantity))
            .Must(x => !x.Any(s => s <= 0))
            .WithMessage("Quantidade do Product é requerido.");

        RuleFor(x => x.Products.Select(p => p.UnitPrice))
            .Must(x => !x.Any(s => s <= 0))
            .WithMessage("Preço unitário do Product é requerido.");

        RuleFor(x => x.Products.Select(p => (int)p.Status))
            .Must(x => !x.Any(s => s <= 0 && s >= 3))
            .WithMessage("Status do Product só pode ser 1 ou 2.");


        RuleFor(r => r.SaleDate)
            .NotEmpty()
            .NotNull()
            .WithMessage("Data da Compra é requerido.");

        RuleFor(r => (int)r.Status)
            .NotNull()
            .ExclusiveBetween(1,3)
            .WithMessage("Status da Compra é requerido.");
    }

}