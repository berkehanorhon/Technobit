using MediatR;
using ProductManagement.DTOs.Read;

namespace ProductManagement.MediatR.Queries;

public record GetProductDetailTagByIdQuery(int Id) : BaseQuery<ProductDetailTagDTO>(Id);