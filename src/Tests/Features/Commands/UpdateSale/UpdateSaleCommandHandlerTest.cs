using Domain.Entities;
using Domain.Features.Commands.UpdateSale;
using Domain.Intefaces;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using Tests.Base;
using Tests.Shared.Commands;
using Tests.Shared.Repository;

namespace Tests.Features.Commands.UpdateSale;

public class UpdateSaleCommandHandlerTest : BaseTest
{
    private readonly Mock<IMongoRepository<SaleEntity>> _repositoryMock;
    private readonly Mock<IMongoRepository<SaleHistoryEntity>> _histRepositoryMock;

    private UpdateSaleCommandValidator UpdateSaleCommandValidator() => new UpdateSaleCommandValidator();

    public UpdateSaleCommandHandlerTest()
    {
        _repositoryMock = new Mock<IMongoRepository<SaleEntity>>();
        _histRepositoryMock = new Mock<IMongoRepository<SaleHistoryEntity>>();
    }

    [Fact]
    public async Task HandleSuccessTest()
    {
        _repositoryMock.Setup(s => s.FindOneAsync(It.IsAny<Expression<Func<SaleEntity, bool>>>())).ReturnsAsync(SaleEntityMoq.SaleEntityResponse);
        _repositoryMock.Setup(s => s.ReplaceOne(It.IsAny<SaleEntity>()));
        _histRepositoryMock.Setup(s => s.InsertOneAsync(It.IsAny<SaleHistoryEntity>()));

        var handler = new UpdateSaleCommandHandler(
            _mapper,
            _repositoryMock.Object,
            _histRepositoryMock.Object);

        //Act
        var response = await handler.Handle(UpdateSaleCommandMoq.updateSaleSuccessRequest, default);

        //Assert
        response.Should().NotBeNull();
        _repositoryMock.Verify(v => v.ReplaceOneAsync(It.IsAny<SaleEntity>()), Times.Once);
    }

    [Fact]
    public async Task HandleValidationError()
    {
        _repositoryMock.Setup(s => s.FindOneAsync(It.IsAny<Expression<Func<SaleEntity, bool>>>())).ReturnsAsync(SaleEntityMoq.SaleEntityResponse);
        _histRepositoryMock.Setup(s => s.InsertOneAsync(It.IsAny<SaleHistoryEntity>()));
        _repositoryMock.Setup(s => s.FindOneAsync(It.IsAny<Expression<Func<SaleEntity, bool>>>()));

        var handler = new UpdateSaleCommandHandler(
            _mapper,
            _repositoryMock.Object,
            _histRepositoryMock.Object);

        var result = await UpdateSaleCommandValidator().ValidateAsync(UpdateSaleCommandMoq.updateSaleValidationNotProductRequest);

        //Assert
        Assert.True(result.Errors.Count >= 1);
        Assert.Contains(result.Errors, x => x.ErrorMessage.Contains("Id do Product é requerido"));
        Assert.Contains(result.Errors, x => x.ErrorMessage.Contains("Quantidade do Product é requerido"));
    }
}
