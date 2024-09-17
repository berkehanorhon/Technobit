using ProductManagement.DTOs.Update;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Update;

namespace ProductManagement.MediatR.Handlers.Commands.Update;

public class UpdateProductCommandHandler : UpdateBaseCommandHandler<IProductService, UpdateProductCommand, UpdateProductDTO>
{
    public UpdateProductCommandHandler(IProductService service) : base(service)
    {
    }

}