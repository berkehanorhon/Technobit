using MediatR;
using ProductManagement.Data;
using ProductManagement.DTOs.Read;
using ProductManagement.DTOs.Update;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Update;

namespace ProductManagement.MediatR.Handlers.Commands.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductDTO>
{
    private readonly IProductService _productService;

    public UpdateProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<UpdateProductDTO> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        return await _productService.UpdateAsync(request.Product);
    }
}