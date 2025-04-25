using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Features.Auth.Commands.LoginCommand;
using System.Threading.Tasks;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/auth")]
[OpenApiTags("Auth")]
public class AuthController : Controller {
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command) {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}
