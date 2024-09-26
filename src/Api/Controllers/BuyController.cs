using Api.Controllers.Base;
using Domain.Contracts;
using Domain.Features.Commands.CreateBuy;
using Domain.Features.Commands.DeleteBuyById;
using Domain.Features.Commands.UpdateBuy;
using Domain.Features.Queries;
using Domain.Features.Queries.GetBuyById;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers;

[Route("api/[controller]")]
public class BuyController : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(PaginationResult<BuyQueryContract>), (int)HttpStatusCode.OK)]

    public async Task<IActionResult> Get([FromQuery] GetAllBuyPaginationQuery query) => Ok(await Mediator.Send(query));

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetBuyByIdQuery), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get(Int64 id)
    {
        return Ok(await Mediator.Send(new GetBuyByIdQuery { BuyId = id }));
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Post(CreateBuyCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Put(UpdateBuyCommand command) => Ok(await Mediator.Send(command));

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(Int64 id) => Ok(await Mediator.Send(new DeleteBuyByIdCommand { BuyId = id }));
}
