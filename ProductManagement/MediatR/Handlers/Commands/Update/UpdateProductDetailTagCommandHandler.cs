using ProductManagement.DTOs.Update;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Update;

namespace ProductManagement.MediatR.Handlers.Commands.Update;

public class UpdateProductDetailTagCommandHandler : UpdateBaseCommandHandler<IProductDetailTagService, UpdateProductDetailTagCommand, UpdateProductDetailTagDTO>
{
    public UpdateProductDetailTagCommandHandler(IProductDetailTagService service) : base(service)
    {
    }

}