using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.DTOs.Read;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Queries;

namespace ProductManagement.MediatR.Handlers.Queries;
public class GetSellerProductByIdQueryHandler : GetBaseByIdQueryHandler<GetSellerProductByIdQuery, SellerProductDTO, ISellerProductService>
{
    public GetSellerProductByIdQueryHandler(ISellerProductService service) : base(service)
    {
    }
}