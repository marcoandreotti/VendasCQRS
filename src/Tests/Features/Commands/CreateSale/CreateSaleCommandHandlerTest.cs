using Domain.Entities;
using Domain.Features.Commands.CreateSale;
using Domain.Intefaces;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using Tests.Base;
using Tests.Shared.Commands;
using Tests.Shared.Repository;

namespace Tests.Features.Commands.CreateSale;

public class CreateSaleCommandHandlerTest : BaseTest
{
    private readonly Mock<IMongoRepository<SaleEntity>> _repositoryMock;
    private readonly Mock<IMongoRepository<SaleHistoryEntity>> _histRepositoryMock;
    private CreateSaleCommandValidator CreateSaleCommandValidator() => new CreateSaleCommandValidator();

    public CreateSaleCommandHandlerTest()
    {
        _repositoryMock = new Mock<IMongoRepository<SaleEntity>>();
        _histRepositoryMock = new Mock<IMongoRepository<SaleHistoryEntity>>();
    }

    [Fact]
    public async Task HandleSuccessTest()
    {
        
        _repositoryMock.Setup(s => s.FindOneAsync(It.IsAny<Expression<Func<SaleEntity, bool>>>())).ReturnsAsync(SaleEntityMoq.SaleEntityResponse);
        _histRepositoryMock.Setup(s => s.InsertOneAsync(It.IsAny<SaleHistoryEntity>()));
        _repositoryMock.Setup(s => s.FindOneAsync(It.IsAny<Expression<Func<SaleEntity, bool>>>()));
        _repositoryMock.Setup(s => s.InsertOneAsync(It.IsAny<SaleEntity>()));

       var handler = new CreateSaleCommandHandler(
            _mapper,
            _repositoryMock.Object,
            _histRepositoryMock.Object);

        //Act
        var response = await handler.Handle(CreateSaleCommandMoq.createSaleSuccessRequest, default);

        //Assert
        response.Should().NotBeNull();
        _repositoryMock.Verify(v => v.InsertOneAsync(It.IsAny<SaleEntity>()), Times.Once);
    }

    [Fact]
    public async Task HandleValidationError()
    {
        _repositoryMock.Setup(s => s.FindOneAsync(It.IsAny<Expression<Func<SaleEntity, bool>>>())).ReturnsAsync(SaleEntityMoq.SaleEntityResponse);
        _histRepositoryMock.Setup(s => s.InsertOneAsync(It.IsAny<SaleHistoryEntity>()));
        _repositoryMock.Setup(s => s.FindOneAsync(It.IsAny<Expression<Func<SaleEntity, bool>>>()));

        var handler = new CreateSaleCommandHandler(
            _mapper,
            _repositoryMock.Object,
            _histRepositoryMock.Object);

        var result = await CreateSaleCommandValidator().ValidateAsync(CreateSaleCommandMoq.createSaleValidationNotProductRequest);

        //Assert
        Assert.True(result.Errors.Count >= 1);
        Assert.Contains(result.Errors, x => x.ErrorMessage.Contains("Id do Product é requerido"));
        Assert.Contains(result.Errors, x => x.ErrorMessage.Contains("Quantidade do Product é requerido"));
    }
}
