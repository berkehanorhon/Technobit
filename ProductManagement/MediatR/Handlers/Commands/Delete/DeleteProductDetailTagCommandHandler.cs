using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Delete;

namespace ProductManagement.MediatR.Handlers.Commands.Delete;

public class DeleteProductDetailTagCommandHandler : DeleteBaseCommandHandler<IProductDetailTagService, DeleteProductDetailTagCommand>
{
    public DeleteProductDetailTagCommandHandler(IProductDetailTagService service) : base(service)
    {
    }

}