using ProductManagement.DTOs.Read;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Queries;

namespace ProductManagement.MediatR.Handlers.Queries;

public class GetCategoryByIdQueryHandler : GetBaseByIdQueryHandler<GetCategoryByIdQuery, CategoryDTO, ICategoryService>
{
    public GetCategoryByIdQueryHandler(ICategoryService service) : base(service)
    {
    }
}