using ProductManagement.DTOs.Read;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Queries;

namespace ProductManagement.MediatR.Handlers.Queries;

public class GetProductDetailTagByIdQueryHandler : GetBaseByIdQueryHandler<GetProductDetailTagByIdQuery, ProductDetailTagDTO, IProductDetailTagService>
{
    public GetProductDetailTagByIdQueryHandler(IProductDetailTagService service) : base(service)
    {
    }
}