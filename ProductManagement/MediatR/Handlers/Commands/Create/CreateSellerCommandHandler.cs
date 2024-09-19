using ProductManagement.DTOs.Create;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Create;

namespace ProductManagement.MediatR.Handlers.Commands.Create;

public class CreateSellerCommandHandler : CreateBaseCommandHandler<ISellerService, CreateSellerCommand, CreateSellerDTO>
{
    public CreateSellerCommandHandler(ISellerService service) : base(service)
    {
    }

}