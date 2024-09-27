using Domain.Entities;
using Domain.Features.Queries;
using Domain.Intefaces;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using Tests.Base;
using Tests.Shared.Queries;

namespace Tests.Features.Queries.GetAllSale;

public class GetAllSalePaginationQueryHandlerTest : BaseTest
{
    private readonly Mock<IMongoRepository<SaleEntity>> _repositoryMock;

    public GetAllSalePaginationQueryHandlerTest()
    {
        _repositoryMock = new Mock<IMongoRepository<SaleEntity>>();
    }

    [Fact]
    public async Task HandleSuccessTest()
    {
        _repositoryMock.Setup(s => s.FilterPaginationBy(1, 1, It.IsAny<Expression<Func<SaleEntity, bool>>>(), null, null)).ReturnsAsync(SaleEntityResponseMoq.listEntity);
        _repositoryMock.Setup(s => s.CountDocuments(It.IsAny<Expression<Func<SaleEntity, bool>>>())).ReturnsAsync(1);

        var handler = new GetAllSalePaginationQueryHandler(_repositoryMock.Object);

        //Act
        var response = await handler.Handle(new GetAllSalePaginationQuery { Page = 1, PageSize = 1 }, default);

        //Assert
        response.Should().NotBeNull();
    }
}
