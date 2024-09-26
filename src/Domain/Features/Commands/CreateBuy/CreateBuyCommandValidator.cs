using FluentValidation;

namespace Domain.Features.Commands.CreateBuy;

public class CreateBuyCommandValidator : AbstractValidator<CreateBuyCommand>
{
    public CreateBuyCommandValidator()
    {
        RuleFor(r => r.BuyId)
            .NotNull()
            .NotEqual(0)
            .WithMessage("{PropertyName} é requerido.")
            .GreaterThan(0)
            .WithMessage("{PropertyName} - não deve ser maior que 0");

        RuleFor(r => r.CustomerId)
            .NotNull()
            .WithMessage("{PropertyName} é requerido.");

        RuleFor(x => x.CustomerId)
            .NotNull()
            .NotEqual(0)
            .WithMessage("{PropertyName} é requerido.")
            .GreaterThan(0)
            .WithMessage("{PropertyName} - não deve ser maior que 0");

        RuleFor(r => r.Products)
            .NotNull()
            .WithMessage("{PropertyName} é requerido.")
            .Must(x => x.Any())
            .WithMessage("{PropertyName} é requerido.");

        RuleFor(x => x.Products.Select(p => p.ProductId))
            .Must(x => x.Any(s => s <= 0))
            .WithMessage("Product {PropertyName} é requerido.");

        RuleFor(x => x.Products.Select(p => p.Quantity))
            .Must(x => !x.Any(s => s <= 0))
            .WithMessage("Product {PropertyName} é requerido.");

        RuleFor(x => x.Products.Select(p => p.UnitPrice))
            .Must(x => !x.Any(s => s <= 0))
            .WithMessage("Product {PropertyName} é requerido.");


        RuleFor(r => r.BuyDate)
            .NotEmpty()
            .WithMessage("{PropertyName} é requerido.")
            .NotNull()
            .WithMessage("{PropertyName} é requerido.");
    }

}