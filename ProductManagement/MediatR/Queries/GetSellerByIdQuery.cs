using MediatR;
using ProductManagement.DTOs.Read;

namespace ProductManagement.MediatR.Queries;

public record GetSellerByIdQuery(int Id) : BaseQuery<SellerDTO>(Id);