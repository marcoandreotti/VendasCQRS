using Api.Controllers.Base;
using Domain.Features.Commands.CreateSale;
using Domain.Features.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
public class SaleController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAllSalesPaginationQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    //[HttpGet("{id}")]
    //public async Task<IActionResult> Get(Int64 id)
    //{
    //    return Ok(await Mediator.Send(new GetSaleByIdQuery { Id = id }));
    //}

    [HttpPost]
    public async Task<IActionResult> Post(CreateSaleCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    //[HttpPut]
    //public async Task<IActionResult> Put(UpdateSaleCommand command)
    //{
    //    return Ok(await Mediator.Send(command));
    //}

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> Delete(Int64 id)
    //{
    //    return Ok(await Mediator.Send(new DeleteSaleByIdCommand { Id = id }));
    //}
}
