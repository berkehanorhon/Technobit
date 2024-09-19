using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.DTOs.Read;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Queries;

namespace ProductManagement.MediatR.Handlers.Queries;
public class GetSellerByIdQueryHandler : GetBaseByIdQueryHandler<GetSellerByIdQuery, SellerDTO, ISellerService>
{
    public GetSellerByIdQueryHandler(ISellerService service) : base(service)
    {
    }
}