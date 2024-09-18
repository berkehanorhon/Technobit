using ProductManagement.DTOs.Create;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Create;

namespace ProductManagement.MediatR.Handlers.Commands.Create;

// TODO bunun gibi başka hatalar olabilir kontrol etmem lazım
public class CreateProductDetailTagCommandHandler : CreateBaseCommandHandler<IProductDetailTagService, CreateProductDetailTagCommand, CreateProductDetailTagDTO>
{
    public CreateProductDetailTagCommandHandler(IProductDetailTagService service) : base(service)
    {
    }

}