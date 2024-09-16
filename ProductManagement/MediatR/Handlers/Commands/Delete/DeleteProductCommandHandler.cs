using MediatR;
using ProductManagement.Data;
using ProductManagement.DTOs.Read;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Delete;

namespace ProductManagement.MediatR.Handlers.Commands.Delete;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IProductService _productService;

    public DeleteProductCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _productService.DeleteAsync(request.Id);
        return Unit.Value;
    }
}
