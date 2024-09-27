﻿using FluentValidation;

namespace Domain.Features.Commands.CreateBuy;

public class CreateBuyCommandValidator : AbstractValidator<CreateBuyCommand>
{
    public CreateBuyCommandValidator()
    {
        RuleFor(r => r.BuyId)
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


        RuleFor(r => r.BuyDate)
            .NotEmpty()
            .NotNull()
            .WithMessage("Data da Compra é requerido.");
    }

}