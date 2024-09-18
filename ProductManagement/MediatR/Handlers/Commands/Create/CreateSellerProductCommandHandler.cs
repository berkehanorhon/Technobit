using ProductManagement.DTOs.Create;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Create;

namespace ProductManagement.MediatR.Handlers.Commands.Create;

public class CreateSellerProductCommandHandler : CreateBaseCommandHandler<ISellerProductService, CreateSellerProductCommand, CreateSellerProductDTO>
{
    public CreateSellerProductCommandHandler(ISellerProductService service) : base(service)
    {
    }

}