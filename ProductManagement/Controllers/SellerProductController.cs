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
[Route("api/[controller]")]
public class SellerProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SellerProductController> _logger;
    public SellerProductController(IMediator mediator, ILogger<SellerProductController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSellertProduct(int id)
    {
        var sellertProduct = await _mediator.Send(new GetSellerProductByIdQuery(id));
        if (sellertProduct == null) return NotFound();
        return Ok(sellertProduct);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateSellertProduct([FromBody] CreateSellerProductDTO sellertProductDto)
    {
        var createdSellertProductId = await _mediator.Send(new CreateSellerProductCommand(sellertProductDto));
        return CreatedAtAction(nameof(GetSellertProduct), new { id = createdSellertProductId }, createdSellertProductId);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateSellertProduct(int id, [FromBody] UpdateSellerProductDTO sellertProductDto)
    {
        if (id != sellertProductDto.Id) return BadRequest();

        var updatedSellertProduct = await _mediator.Send(new UpdateSellerProductCommand(sellertProductDto));
        if (updatedSellertProduct == null) return NotFound();
        return Ok(updatedSellertProduct);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteSellertProduct(int id)
    {
        await _mediator.Send(new DeleteSellerProductCommand(id));
        return NoContent();
    }
}