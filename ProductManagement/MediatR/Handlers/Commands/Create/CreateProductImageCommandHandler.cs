using ProductManagement.DTOs.Create;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Create;

namespace ProductManagement.MediatR.Handlers.Commands.Create;

public class CreateProductImageCommandHandler : CreateBaseCommandHandler<IProductImageService, CreateProductImageCommand, CreateProductImageDTO>
{
    public CreateProductImageCommandHandler(IProductImageService service) : base(service)
    {
    }

}