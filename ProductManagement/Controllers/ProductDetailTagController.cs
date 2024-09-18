using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.DTOs.Create;
using ProductManagement.DTOs.Update;
using ProductManagement.MediatR.Commands.Create;
using ProductManagement.MediatR.Commands.Delete;
using ProductManagement.MediatR.Commands.Update;
using ProductManagement.MediatR.Queries;

namespace ProductManagement.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProductDetailTagController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductDetailTagController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductDetailTag(int id)
    {
        var productDetailTag = await _mediator.Send(new GetProductDetailTagByIdQuery(id));
        if (productDetailTag == null) return NotFound();
        return Ok(productDetailTag);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductDetailTag([FromBody] CreateProductDetailTagDTO productDetailTagDto)
    {
        var createdProductDetailTagId = await _mediator.Send(new CreateProductDetailTagCommand(productDetailTagDto));
        return CreatedAtAction(nameof(GetProductDetailTag), new { id = createdProductDetailTagId }, createdProductDetailTagId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductDetailTag(int id, [FromBody] UpdateProductDetailTagDTO productDetailTagDto)
    {
        if (id != productDetailTagDto.Id) return BadRequest();

        var updatedProductDetailTag = await _mediator.Send(new UpdateProductDetailTagCommand(productDetailTagDto));
        if (updatedProductDetailTag == null) return NotFound();
        return Ok(updatedProductDetailTag);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductDetailTag(int id)
    {
        await _mediator.Send(new DeleteProductDetailTagCommand(id));
        return NoContent();
    }
}