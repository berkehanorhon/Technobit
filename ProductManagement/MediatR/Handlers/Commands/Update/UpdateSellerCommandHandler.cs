using ProductManagement.DTOs.Update;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Update;

namespace ProductManagement.MediatR.Handlers.Commands.Update;

public class UpdateSellerCommandHandler : UpdateBaseCommandHandler<ISellerService, UpdateSellerCommand, UpdateSellerDTO>
{
    public UpdateSellerCommandHandler(ISellerService service) : base(service)
    {
    }

}