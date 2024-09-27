using MediatR;
using ProductManagement.DTOs.Read;

namespace ProductManagement.MediatR.Queries;

public record GetProductDetailsByIdQuery(int Id) : BaseQuery<ProductPageSendDTO>(Id);