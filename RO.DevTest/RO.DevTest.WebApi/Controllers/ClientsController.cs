using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Features.Client.Commands.CreateClientCommand;
using RO.DevTest.Application.Features.Client.Commands.UpdateClientCommand;
using RO.DevTest.Application.Features.Client.Commands.DeleteClientCommand;
using RO.DevTest.Application.Features.Client.Queries.GetClientsQuery;
using System;
using System.Threading.Tasks;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/clients")]
[OpenApiTags("Clients")]
public class ClientsController : Controller {
    private readonly IMediator _mediator;

    public ClientsController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] CreateClientCommand command) {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetClientById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClientById(Guid id) {
        var result = await _mediator.Send(new GetClientByIdQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetClients([FromQuery] GetClientsQuery query) {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient(Guid id, [FromBody] UpdateClientCommand command) {
        if (id != command.Id) return BadRequest();
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(Guid id) {
        await _mediator.Send(new DeleteClientCommand { Id = id });
        return NoContent();
    }
}
