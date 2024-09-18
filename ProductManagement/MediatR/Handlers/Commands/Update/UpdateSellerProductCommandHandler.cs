using ProductManagement.DTOs.Update;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Update;

namespace ProductManagement.MediatR.Handlers.Commands.Update;

public class UpdateSellerProductCommandHandler : UpdateBaseCommandHandler<ISellerProductService, UpdateSellerProductCommand, UpdateSellerProductDTO>
{
    public UpdateSellerProductCommandHandler(ISellerProductService service) : base(service)
    {
    }

}