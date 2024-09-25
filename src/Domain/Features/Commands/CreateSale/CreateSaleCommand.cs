using Domain.Contracts;
using FluentValidation;
using MediatR;

namespace Domain.Features.Commands.CreateSale;

public class CreateSaleCommand : SaleContract, IRequest<Unit> { }

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(r => r.SaleId)
            .NotNull()
            .WithMessage("{PropertyName} is required.")
            .GreaterThan(0)
            .WithMessage("{PropertyName} - Number of trainings must be greater than 0.");

        RuleFor(r => r.Customer)
            .NotNull()
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.Customer.CustomerId)
            .NotNull().NotEmpty()
            .WithMessage("Customer {PropertyName} is required.")
            .Must(x => x <= 0)
            .WithMessage("Customer {PropertyName} is required.");

        RuleFor(x => x.Customer.Name)
            .NotNull().NotEmpty()
            .WithMessage("Customer {PropertyName} is required.");

        RuleFor(r => r.Products)
            .NotNull()
            .WithMessage("{PropertyName} is required.")
            .Must(x => x.Any())
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.Products.Select(p => p.ProductId))
            .Must(x => !x.Any(s => s <= 0))
            .WithMessage("Product {PropertyName} is required.");

        RuleFor(x => x.Products.Select(p => p.Name))
            .Must(x => !x.Any(s => string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s)))
            .WithMessage("Product {PropertyName} is required.");

        RuleFor(x => x.Products.Select(p => p.Quantity))
            .Must(x => !x.Any(s => s <= 0))
            .WithMessage("Product {PropertyName} is required.");

        RuleFor(x => x.Products.Select(p => p.UnitPrice))
            .Must(x => !x.Any(s => s <= 0))
            .WithMessage("Product {PropertyName} is required.");


        RuleFor(r => r.SaleDate)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .WithMessage("{PropertyName} is required.");
    }

    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Unit>
    {
        public CreateSaleCommandHandler()
        {
        }

        public async Task<Unit> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            return new Unit();
        }
    }
}

