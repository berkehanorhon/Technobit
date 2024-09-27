using MediatR;
using ProductManagement.DTOs.Read;
using ProductManagement.Models;

namespace ProductManagement.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<(List<ProductSendListDTO> Items, int TotalPages)> GetLatestsWithPaging(ProductListingDTO dto);
    Task<ProductPageSendDTO?> GetProductPageSendDTOAsync(int productId);
}