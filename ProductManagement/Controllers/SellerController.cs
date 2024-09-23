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
public class SellerController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SellerController> _logger;
    public SellerController(IMediator mediator, ILogger<SellerController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSeller(int id)
    {
        var seller = await _mediator.Send(new GetSellerByIdQuery(id));
        if (seller == null) return NotFound();
        return Ok(seller);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateSeller([FromBody] CreateSellerDTO sellerDto)
    {
        var createdSellerId = await _mediator.Send(new CreateSellerCommand(sellerDto));
        return CreatedAtAction(nameof(GetSeller), new { id = createdSellerId }, createdSellerId);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateSeller(int id, [FromBody] UpdateSellerDTO sellerDto)
    {
        if (id != sellerDto.Id) return BadRequest();

        var updatedSeller = await _mediator.Send(new UpdateSellerCommand(sellerDto));
        if (updatedSeller == null) return NotFound();
        return Ok(updatedSeller);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteSeller(int id)
    {
        await _mediator.Send(new DeleteSellerCommand(id));
        return NoContent();
    }
}