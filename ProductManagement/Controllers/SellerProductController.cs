using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.DTOs.Create;
using ProductManagement.DTOs.Update;
using ProductManagement.MediatR.Commands.Create;
using ProductManagement.MediatR.Commands.Create.FromFile;
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
    public async Task<IActionResult> GetSellerProduct(int id)
    {
        var sellertProduct = await _mediator.Send(new GetSellerProductByIdQuery(id));
        if (sellertProduct == null) return NotFound();
        return Ok(sellertProduct);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateSellerProduct([FromBody] CreateSellerProductDTO sellertProductDto)
    {
        var createdSellertProductId = await _mediator.Send(new CreateSellerProductCommand(sellertProductDto));
        return CreatedAtAction(nameof(GetSellerProduct), new { id = createdSellertProductId }, createdSellertProductId);
    }
    
    [Authorize]
    [HttpPost("import")]
    public async Task<IActionResult> ImportProducts([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded or file is empty.");
        }

        var command = new ImportProductsCommand(file);
        var result = await _mediator.Send(command);

        if (result)
        {
            return Ok("Products imported successfully.");
        }

        return BadRequest("Failed to import one or more products.");
    }
    
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateSellerProduct(int id, [FromBody] UpdateSellerProductDTO sellertProductDto)
    {
        if (id != sellertProductDto.Id) return BadRequest();

        var updatedSellertProduct = await _mediator.Send(new UpdateSellerProductCommand(sellertProductDto));
        if (updatedSellertProduct == null) return NotFound();
        return Ok(updatedSellertProduct);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteSellerProduct(int id)
    {
        await _mediator.Send(new DeleteSellerProductCommand(id));
        return NoContent();
    }
}