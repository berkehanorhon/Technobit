using MediatR;
using ProductManagement.DTOs.Create;
using ProductManagement.DTOs.Read;

namespace ProductManagement.MediatR.Queries;

public record GetCategoryByIdQuery(int Id) : BaseQuery<CategoryDTO>(Id);

