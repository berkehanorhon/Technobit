using ProductManagement.DTOs.Create;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Create;

namespace ProductManagement.MediatR.Handlers.Commands.Create;
public class CreateProductDetailTagCommandHandler : CreateBaseCommandHandler<IProductService, CreateProductDetailTagCommand, CreateProductDetailTagDTO>
{
    public CreateProductDetailTagCommandHandler(IProductService service) : base(service)
    {
    }

}