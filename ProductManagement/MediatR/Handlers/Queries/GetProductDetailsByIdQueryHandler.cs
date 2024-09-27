using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.DTOs.Read;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Queries;

namespace ProductManagement.MediatR.Handlers.Queries;

public class GetProductDetailsByIdQueryHandler : IRequestHandler<GetProductDetailsByIdQuery, ProductPageSendDTO>
{
    private readonly IProductService _service;

    public GetProductDetailsByIdQueryHandler(IProductService service)
    {
        _service = service;
    }

    public async Task<ProductPageSendDTO?> Handle(GetProductDetailsByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _service.GetDetailsById(request.Id);
    }
}