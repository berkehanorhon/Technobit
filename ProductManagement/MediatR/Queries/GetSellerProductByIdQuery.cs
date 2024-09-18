using MediatR;
using ProductManagement.DTOs.Read;

namespace ProductManagement.MediatR.Queries;

public record GetSellerProductByIdQuery(int Id) : BaseQuery<SellerProductDTO>(Id);