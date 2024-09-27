using MediatR;
using ProductManagement.DTOs.Create;
using ProductManagement.DTOs.Read;
using ProductManagement.DTOs.Update;
using ProductManagement.Interfaces;
using ProductManagement.Interfaces.Repositories;
using ProductManagement.Models;

namespace ProductManagement.Services;


public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDTO>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products.Select(p => new ProductDTO
        {
            Id = p.Id,
            Categoryid = p.Categoryid,
            Name = p.Name,
            Description = p.Description
        });
    }

    public async Task<ProductDTO?> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return null;

        return new ProductDTO
        {
            Id = product.Id,
            Categoryid = product.Categoryid,
            Name = product.Name,
            Description = product.Description
        };
    }

    public async Task<ProductPageSendDTO?> GetDetailsById(int productId)
    {
        return await _productRepository.GetProductPageSendDTOAsync(productId);
    }
    
    public async Task<int> CreateAsync(CreateProductDTO createProductDto)
    {
        var product = new Product
        {
            Categoryid = createProductDto.Categoryid,
            Name = createProductDto.Name,
            Description = createProductDto.Description
        };

        await _productRepository.AddAsync(product);

        return product.Id;
    }

    public async Task<UpdateProductDTO> UpdateAsync(UpdateProductDTO productDto)
    {
        var product = await _productRepository.GetByIdAsync(productDto.Id);
        if (product == null) return null;

        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Categoryid = productDto.Categoryid;

        await _productRepository.UpdateAsync(product);

        return productDto;
    }

    public async Task<Unit> DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product != null)
        {
            await _productRepository.DeleteAsync(product);
        }
        return Unit.Value;
    }

    public async Task<(List<ProductSendListDTO> Items, int TotalPages)> GetLatestsWithPaging(ProductListingDTO dto)
    {
        return await _productRepository.GetLatestsWithPaging(dto);
    }
}