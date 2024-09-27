using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.DTOs.Create;
using ProductManagement.DTOs.Read;
using ProductManagement.DTOs.Update;
using ProductManagement.MediatR.Commands.Create;
using ProductManagement.MediatR.Commands.Delete;
using ProductManagement.MediatR.Commands.Update;
using ProductManagement.MediatR.Queries;

namespace ProductManagement.Controllers;

[ApiController]
[Route("api/requests/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductController> _logger;
    public ProductController(IMediator mediator, ILogger<ProductController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetProduct([FromQuery] ProductListingDTO dto)
    { 
        if (dto.count <= 0 || dto.pageNumber <= 0)
        {
            return BadRequest("Count and Page number must be greater than 0.");
        }

        var (items, totalPages) = await _mediator.Send(new GetProductListQuery(dto));

        if (!items.Any())
        {
            return NotFound("No products found for the given page.");
        }

        var response = new
        {
            Items = items,
            CurrentPage = dto.pageNumber,
            TotalPages = totalPages,
        };

        return Ok(response);
    }
    
    [HttpGet("details")]
    public async Task<IActionResult> GetProductDetails(int id)
    {
        var productPageSendDTO = await _mediator.Send(new GetProductDetailsByIdQuery(id));
        if (productPageSendDTO == null)
        {
            return NotFound();
        }
        return Ok(productPageSendDTO);
    }
    
    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO productDto)
    {
        var createdProductId = await _mediator.Send(new CreateProductCommand(productDto));
        return CreatedAtAction(nameof(GetProduct), new { id = createdProductId }, createdProductId);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDTO productDto)
    {
        if (id != productDto.Id) return BadRequest();

        var updatedProduct = await _mediator.Send(new UpdateProductCommand(productDto));
        if (updatedProduct == null) return NotFound();
        return Ok(updatedProduct);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }
}