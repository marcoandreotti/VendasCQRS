using Api.Controllers.Base;
using Domain.Contracts;
using Domain.Features.Commands.CreateSale;
using Domain.Features.Commands.DeleteSaleById;
using Domain.Features.Commands.UpdateSale;
using Domain.Features.Queries;
using Domain.Features.Queries.GetSaleById;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers;

[Route("api/[controller]")]
public class SaleController : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(PaginationResult<SaleQueryContract>), (int)HttpStatusCode.OK)]

    public async Task<IActionResult> Get([FromQuery] GetAllSalePaginationQuery query) => Ok(await Mediator.Send(query));

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetSaleByIdQuery), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get(Int64 id)
    {
        return Ok(await Mediator.Send(new GetSaleByIdQuery { SaleId = id }));
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Post(CreateSaleCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Put(UpdateSaleCommand command) => Ok(await Mediator.Send(command));

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(Int64 id) => Ok(await Mediator.Send(new DeleteSaleByIdCommand { SaleId = id }));
}
