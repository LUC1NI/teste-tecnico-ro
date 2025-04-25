using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Features.Sale.Commands.CreateSaleCommand;
using RO.DevTest.Application.Features.Sale.Commands.UpdateSaleCommand;
using RO.DevTest.Application.Features.Sale.Commands.DeleteSaleCommand;
using RO.DevTest.Application.Features.Sale.Queries.GetSalesQuery;
using RO.DevTest.Application.Features.Sale.Queries.GetSaleByIdQuery;
using System;
using System.Threading.Tasks;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/sales")]
[OpenApiTags("Sales")]
public class SalesController : Controller {
    private readonly IMediator _mediator;

    public SalesController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand command) {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetSaleById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSaleById(Guid id) {
        var result = await _mediator.Send(new GetSaleByIdQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetSales([FromQuery] GetSalesQuery query) {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSale(Guid id, [FromBody] UpdateSaleCommand command) {
        if (id != command.Id) return BadRequest();
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSale(Guid id) {
        await _mediator.Send(new DeleteSaleCommand { Id = id });
        return NoContent();
    }

    [HttpGet("analysis")]
    public async Task<IActionResult> AnalyzeSales([FromQuery] DateTime startDate, [FromQuery] DateTime endDate) {
        var result = await _mediator.Send(new RO.DevTest.Application.Features.Sale.Queries.AnalyzeSalesQuery.AnalyzeSalesQuery {
            StartDate = startDate,
            EndDate = endDate
        });
        return Ok(result);
    }
}
