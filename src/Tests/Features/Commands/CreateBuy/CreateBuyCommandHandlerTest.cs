using Domain.Entities;
using Domain.Features.Commands.CreateBuy;
using Domain.Intefaces;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using Tests.Base;
using Tests.Shared.Commands;
using Tests.Shared.Repository;

namespace Tests.Features.Commands.CreateBuy;

public class CreateBuyCommandHandlerTest : BaseTest
{
    private readonly Mock<IMongoRepository<BuyEntity>> _repositoryMock;
    private readonly Mock<IMongoRepository<BuyHistoryEntity>> _histRepositoryMock;
    private CreateBuyCommandValidator CreateBuyCommandValidator() => new CreateBuyCommandValidator();

    public CreateBuyCommandHandlerTest()
    {
        _repositoryMock = new Mock<IMongoRepository<BuyEntity>>();
        _histRepositoryMock = new Mock<IMongoRepository<BuyHistoryEntity>>();
    }

    [Fact]
    public async Task HandleSuccessTest()
    {
        
        _repositoryMock.Setup(s => s.FindOneAsync(It.IsAny<Expression<Func<BuyEntity, bool>>>())).ReturnsAsync(BuyEntityMoq.BuyEntityResponse);
        _histRepositoryMock.Setup(s => s.InsertOneAsync(It.IsAny<BuyHistoryEntity>()));
        _repositoryMock.Setup(s => s.FindOneAsync(It.IsAny<Expression<Func<BuyEntity, bool>>>()));
        _repositoryMock.Setup(s => s.InsertOneAsync(It.IsAny<BuyEntity>()));

       var handler = new CreateBuyCommandHandler(
            _mapper,
            _repositoryMock.Object,
            _histRepositoryMock.Object);

        //Act
        var response = await handler.Handle(CreateBuyCommandMoq.createBuySuccessRequest, default);

        //Assert
        response.Should().NotBeNull();
        _repositoryMock.Verify(v => v.InsertOneAsync(It.IsAny<BuyEntity>()), Times.Once);
    }

    [Fact]
    public async Task HandleValidationError()
    {
        _repositoryMock.Setup(s => s.FindOneAsync(It.IsAny<Expression<Func<BuyEntity, bool>>>())).ReturnsAsync(BuyEntityMoq.BuyEntityResponse);
        _histRepositoryMock.Setup(s => s.InsertOneAsync(It.IsAny<BuyHistoryEntity>()));
        _repositoryMock.Setup(s => s.FindOneAsync(It.IsAny<Expression<Func<BuyEntity, bool>>>()));

        var handler = new CreateBuyCommandHandler(
            _mapper,
            _repositoryMock.Object,
            _histRepositoryMock.Object);

        var result = await CreateBuyCommandValidator().ValidateAsync(CreateBuyCommandMoq.createBuyValidationNotProductRequest);

        //Assert
        Assert.True(result.Errors.Count >= 1);
        Assert.Contains(result.Errors, x => x.ErrorMessage.Contains("Id do Product é requerido"));
        Assert.Contains(result.Errors, x => x.ErrorMessage.Contains("Quantidade do Product é requerido"));
    }
}
