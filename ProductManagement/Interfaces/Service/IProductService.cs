using ProductManagement.DTOs.Create;
using ProductManagement.DTOs.Read;
using ProductManagement.DTOs.Update;
using ProductManagement.Models;

namespace ProductManagement.Interfaces;

public interface IProductService : IService<CreateProductDTO, ProductDTO, UpdateProductDTO>
{
    Task<(List<ProductSendListDTO> Items, int TotalPages)> GetLatestsWithPaging(ProductListingDTO dto);
    Task<ProductPageSendDTO?> GetDetailsById(int productId);
}
