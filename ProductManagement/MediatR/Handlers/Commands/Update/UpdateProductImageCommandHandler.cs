using ProductManagement.DTOs.Update;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Update;

namespace ProductManagement.MediatR.Handlers.Commands.Update;

public class UpdateProductImageCommandHandler : UpdateBaseCommandHandler<IProductImageService, UpdateProductImageCommand, UpdateProductImageDTO>
{
    public UpdateProductImageCommandHandler(IProductImageService service) : base(service)
    {
    }

}