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
[Route("api/requests/[controller]")]
public class ProductImageController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductImageController> _logger;
    public ProductImageController(IMediator mediator, ILogger<ProductImageController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductImage(int id)
    {
        var productImage = await _mediator.Send(new GetProductImageByIdQuery(id));
        if (productImage == null) return NotFound();
        return Ok(productImage);
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> CreateProductImage([FromBody] CreateProductImageDTO productImageDto)
    {
        var createdProductImageId = await _mediator.Send(new CreateProductImageCommand(productImageDto));
        return CreatedAtAction(nameof(GetProductImage), new { id = createdProductImageId }, createdProductImageId);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> UpdateProductImage(int id, [FromBody] UpdateProductImageDTO productImageDto)
    {
        if (id != productImageDto.Id) return BadRequest();

        var updatedProductImage = await _mediator.Send(new UpdateProductImageCommand(productImageDto));
        if (updatedProductImage == null) return NotFound();
        return Ok(updatedProductImage);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeleteProductImage(int id)
    {
        await _mediator.Send(new DeleteProductImageCommand(id));
        return NoContent();
    }
}