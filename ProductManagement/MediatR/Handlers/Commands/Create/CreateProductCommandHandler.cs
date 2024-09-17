using MediatR;
using ProductManagement.Data;
using ProductManagement.DTOs.Create;
using ProductManagement.DTOs.Read;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Create;
using ProductManagement.Models;

namespace ProductManagement.MediatR.Handlers.Commands.Create;

public class CreateProductCommandHandler : CreateBaseCommandHandler<IProductService, CreateProductCommand, CreateProductDTO>
{
    public CreateProductCommandHandler(IProductService service) : base(service)
    {
    }

}