using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.DTOs.Read;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Queries;

namespace ProductManagement.MediatR.Handlers.Queries;
public class GetProductByIdQueryHandler : GetBaseByIdQueryHandler<GetProductByIdQuery, ProductDTO, IProductService>
{
    public GetProductByIdQueryHandler(IProductService service) : base(service)
    {
    }
}