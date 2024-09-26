using ProductManagement.DTOs.Read;

namespace ProductManagement.MediatR.Queries;

public record GetProductImageByIdQuery(int Id) : BaseQuery<ProductImageDTO>(Id);