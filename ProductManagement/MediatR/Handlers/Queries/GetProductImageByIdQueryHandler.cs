using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.DTOs.Read;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Queries;

namespace ProductManagement.MediatR.Handlers.Queries;
public class GetProductImageByIdQueryHandler : GetBaseByIdQueryHandler<GetProductImageByIdQuery, ProductImageDTO, IProductImageService>
{
    public GetProductImageByIdQueryHandler(IProductImageService service) : base(service)
    {
    }
}