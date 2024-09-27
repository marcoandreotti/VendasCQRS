using Domain.Entities;
using Domain.Features.Commands.DeleteSaleById;
using Domain.Features.Commands.UpdateSale;
using Domain.Intefaces;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using Tests.Base;
using Tests.Shared.Commands;
using Tests.Shared.Repository;

namespace Tests.Features.Commands.UpdateSale;

public class DeleteSaleByIdCommandHandlerTest : BaseTest
{
    private readonly Mock<IMongoRepository<SaleEntity>> _repositoryMock;
    private readonly Mock<IMongoRepository<SaleHistoryEntity>> _histRepositoryMock;

    public DeleteSaleByIdCommandHandlerTest()
    {
        _repositoryMock = new Mock<IMongoRepository<SaleEntity>>();
        _histRepositoryMock = new Mock<IMongoRepository<SaleHistoryEntity>>();
    }

    [Fact]
    public async Task HandleSuccessTest()
    {
        _repositoryMock.Setup(s => s.DeleteOneAsync(It.IsAny<Expression<Func<SaleEntity, bool>>>()));
        _histRepositoryMock.Setup(s => s.InsertOneAsync(It.IsAny<SaleHistoryEntity>()));

        var handler = new DeleteSaleByIdCommandHandler(
            _mapper,
            _repositoryMock.Object,
            _histRepositoryMock.Object);

        //Act
        var response = await handler.Handle(new DeleteSaleByIdCommand { SaleId = 1 }, default);

        //Assert
        response.Should().NotBeNull();
        _repositoryMock.Verify(v => v.DeleteOneAsync(It.IsAny<Expression<Func<SaleEntity, bool>>>()), Times.Once);
    }

    
}
