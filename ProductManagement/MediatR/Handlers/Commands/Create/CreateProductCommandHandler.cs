using MediatR;
using ProductManagement.Data;
using ProductManagement.DTOs.Create;
using ProductManagement.DTOs.Read;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Create;
using ProductManagement.Models;

namespace ProductManagement.MediatR.Handlers.Commands.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDTO>
{
    private readonly IProductService _productService;

    public CreateProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<ProductDTO> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        return await _productService.CreateAsync(request.Product);
    }
}