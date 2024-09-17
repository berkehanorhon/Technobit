using MediatR;
using ProductManagement.Data;
using ProductManagement.DTOs.Read;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Delete;

namespace ProductManagement.MediatR.Handlers.Commands.Delete;

public class DeleteProductCommandHandler : DeleteBaseCommandHandler<IProductService, DeleteProductCommand>
{
    public DeleteProductCommandHandler(IProductService service) : base(service)
    {
    }

}