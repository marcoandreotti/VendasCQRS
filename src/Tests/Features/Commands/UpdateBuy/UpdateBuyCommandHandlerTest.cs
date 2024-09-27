using Domain.Entities;
using Domain.Features.Commands.UpdateBuy;
using Domain.Intefaces;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using Tests.Base;
using Tests.Shared.Commands;
using Tests.Shared.Repository;

namespace Tests.Features.Commands.UpdateBuy;

public class UpdateBuyCommandHandlerTest : BaseTest
{
    private readonly Mock<IMongoRepository<BuyEntity>> _repositoryMock;
    private readonly Mock<IMongoRepository<BuyHistoryEntity>> _histRepositoryMock;
    private UpdateBuyCommandValidator UpdateBuyCommandValidator() => new UpdateBuyCommandValidator();

    public UpdateBuyCommandHandlerTest()
    {
        _repositoryMock = new Mock<IMongoRepository<BuyEntity>>();
        _histRepositoryMock = new Mock<IMongoRepository<BuyHistoryEntity>>();
    }

    [Fact]
    public async Task HandleSuccessTest()
    {

        _repositoryMock.Setup(s => s.FindOneAsync(It.IsAny<Expression<Func<BuyEntity, bool>>>())).ReturnsAsync(BuyEntityMoq.BuyEntityResponse);
        _repositoryMock.Setup(s => s.ReplaceOne(It.IsAny<BuyEntity>()));
        _histRepositoryMock.Setup(s => s.InsertOneAsync(It.IsAny<BuyHistoryEntity>()));

        var handler = new UpdateBuyCommandHandler(
            _mapper,
            _repositoryMock.Object,
            _histRepositoryMock.Object);

        //Act
        var response = await handler.Handle(UpdateBuyCommandMoq.updateBuySuccessRequest, default);

        //Assert
        response.Should().NotBeNull();
        _repositoryMock.Verify(v => v.ReplaceOneAsync(It.IsAny<BuyEntity>()), Times.Once);
    }

    [Fact]
    public async Task HandleValidationError()
    {
        _repositoryMock.Setup(s => s.FindOneAsync(It.IsAny<Expression<Func<BuyEntity, bool>>>())).ReturnsAsync(BuyEntityMoq.BuyEntityResponse);
        _histRepositoryMock.Setup(s => s.InsertOneAsync(It.IsAny<BuyHistoryEntity>()));
        _repositoryMock.Setup(s => s.FindOneAsync(It.IsAny<Expression<Func<BuyEntity, bool>>>()));

        var handler = new UpdateBuyCommandHandler(
            _mapper,
            _repositoryMock.Object,
            _histRepositoryMock.Object);

        var result = await UpdateBuyCommandValidator().ValidateAsync(UpdateBuyCommandMoq.updateBuyValidationNotProductRequest);

        //Assert
        Assert.True(result.Errors.Count >= 1);
        Assert.Contains(result.Errors, x => x.ErrorMessage.Contains("Id do Product é requerido"));
        Assert.Contains(result.Errors, x => x.ErrorMessage.Contains("Quantidade do Product é requerido"));
    }
}
