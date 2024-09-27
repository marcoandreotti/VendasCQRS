using AutoMapper;
using Domain.Entities;
using Domain.Features.Queries.GetSaleById;
using Domain.Intefaces;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using Tests.Base;
using Tests.Shared.Queries;

namespace Tests.Features.Queries.GetSaleById;

public class GetSaleByIdQueryHandlerTest : BaseTest
{
    private readonly Mock<IMongoRepository<SaleEntity>> _repositoryMock;

    public GetSaleByIdQueryHandlerTest()
    {
        _repositoryMock = new Mock<IMongoRepository<SaleEntity>>();
    }

    [Fact]
    public async Task HandleSuccessTest()
    {
        _repositoryMock.Setup(s => s.FindOneAsync(It.IsAny<Expression<Func<SaleEntity, bool>>>())).ReturnsAsync(SaleEntityResponseMoq.entity);

        var handler = new GetSaleByIdQueryHandler(
            _mapper,
            _repositoryMock.Object);

        //Act
        var response = await handler.Handle(new GetSaleByIdQuery(), default);

        //Assert
        response.Should().NotBeNull();
    }
}
