using MediatR;
using ProductManagement.DTOs.Read;
using ProductManagement.Interfaces;
using ProductManagement.Models;

namespace ProductManagement.MediatR.Queries;

public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, (List<ProductSendListDTO> Items, int TotalPages)>

{
    private readonly IProductService _service;

    public GetProductListQueryHandler(IProductService service)
    {
        _service = service;
    }

    public async Task<(List<ProductSendListDTO> Items, int TotalPages)> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetLatestsWithPaging(request.dto);
    }
}