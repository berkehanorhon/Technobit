using MediatR;
using ProductManagement.DTOs.Create;
using ProductManagement.DTOs.Read;
using ProductManagement.DTOs.Update;
using ProductManagement.Interfaces;
using ProductManagement.Models;

namespace ProductManagement.Services;


public class ProductImageService : IProductImageService
{
    private readonly IRepository<Productimage> _productImageRepository;

    public ProductImageService(IRepository<Productimage> productImageRepository)
    {
        _productImageRepository = productImageRepository;
    }

    public async Task<IEnumerable<ProductImageDTO>> GetAllAsync()
    {
        var categories = await _productImageRepository.GetAllAsync();
        return categories.Select(p => new ProductImageDTO
        {
            Id = p.Id,
            Productid = p.Productid,
            Imagepath = p.Imagepath
        });
    }

    public async Task<ProductImageDTO?> GetByIdAsync(int id)
    {
        var productImage = await _productImageRepository.GetByIdAsync(id);
        if (productImage == null) return null;

        return new ProductImageDTO
        {
            Id = productImage.Id,
            Productid = productImage.Productid,
            Imagepath = productImage.Imagepath,
        };
    }

    public async Task<int> CreateAsync(CreateProductImageDTO createProductImageDto)
    {
        var productImage = new Productimage
        {
            Productid = createProductImageDto.Productid,
            Imagepath = createProductImageDto.Imagepath
        };

        await _productImageRepository.AddAsync(productImage);

        return productImage.Id;
    }

    public async Task<UpdateProductImageDTO> UpdateAsync(UpdateProductImageDTO productImageDto)
    {
        var productImage = await _productImageRepository.GetByIdAsync(productImageDto.Id);
        if (productImage == null) return null;

        productImage.Productid = productImageDto.Productid;
        productImage.Imagepath = productImageDto.Imagepath;

        await _productImageRepository.UpdateAsync(productImage);

        return productImageDto;
    }

    public async Task<Unit> DeleteAsync(int id)
    {
        var productImage = await _productImageRepository.GetByIdAsync(id);
        if (productImage != null)
        {
            await _productImageRepository.DeleteAsync(productImage);
        }
        return Unit.Value;
    }
}