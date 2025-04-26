using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;
using RO.DevTest.Application.Features.Product.Commands.UpdateProductCommand;
using RO.DevTest.Application.Features.Product.Commands.DeleteProductCommand;
using RO.DevTest.Application.Features.Product.Queries.GetProductsQuery;
using System;
using System.Threading.Tasks;
using RO.DevTest.Application.Features.Product.Queries.GetProductByIdQuery;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/products")]
[OpenApiTags("Products")]
public class ProductsController : Controller {
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command) {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id) {
        var result = await _mediator.Send(new GetProductByIdQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery query) {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand command) {
        if (id != command.Id) return BadRequest();
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id) {
        await _mediator.Send(new DeleteProductCommand { Id = id });
        return NoContent();
    }
}
