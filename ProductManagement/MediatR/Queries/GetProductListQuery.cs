using MediatR;
using ProductManagement.DTOs.Read;
using ProductManagement.Models;

namespace ProductManagement.MediatR.Queries;

public record GetProductListQuery(ProductListingDTO dto) : IRequest<(List<ProductSendListDTO> Items, int TotalPages)>;