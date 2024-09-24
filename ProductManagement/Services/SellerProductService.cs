using MediatR;
using ProductManagement.DTOs.Create;
using ProductManagement.DTOs.Read;
using ProductManagement.DTOs.Update;
using ProductManagement.Interfaces;
using ProductManagement.Models;

namespace ProductManagement.Services;


public class SellerProductService : ISellerProductService
{
    private readonly IRepository<Sellerproduct> _sellerProductRepository;

    public SellerProductService(IRepository<Sellerproduct> sellerProductRepository)
    {
        _sellerProductRepository = sellerProductRepository;
    }

    public async Task<IEnumerable<SellerProductDTO>> GetAllAsync()
    {
        var categories = await _sellerProductRepository.GetAllAsync();
        return categories.Select(p => new SellerProductDTO
        {
            Id = p.Id,
            Productid = p.Productid,
            Sellerid = p.Sellerid,
            Price = p.Price,
            Stockquantity = p.Stockquantity
        });
    }

    public async Task<SellerProductDTO?> GetByIdAsync(int id)
    {
        var sellerProduct = await _sellerProductRepository.GetByIdAsync(id);
        if (sellerProduct == null) return null;

        return new SellerProductDTO
        {
            Id = sellerProduct.Id,
            Productid = sellerProduct.Productid,
            Sellerid = sellerProduct.Sellerid,
            Price = sellerProduct.Price,
            Stockquantity = sellerProduct.Stockquantity
        };
    }

    public async Task<int> CreateAsync(CreateSellerProductDTO createSellerProductDto)
    {
        var sellerProduct = new Sellerproduct
        {
            Productid = createSellerProductDto.Productid,
            Sellerid = createSellerProductDto.Sellerid,
            Price = createSellerProductDto.Price,
            Stockquantity = createSellerProductDto.Stockquantity
        };

        await _sellerProductRepository.AddAsync(sellerProduct);

        return sellerProduct.Id;
    }

    public async Task<UpdateSellerProductDTO> UpdateAsync(UpdateSellerProductDTO sellerProductDto)
    {
        var sellerProduct = await _sellerProductRepository.GetByIdAsync(sellerProductDto.Id);
        if (sellerProduct == null) return null;

        sellerProduct.Productid = sellerProductDto.Productid;
        sellerProduct.Sellerid = sellerProductDto.Sellerid;
        sellerProduct.Price = sellerProductDto.Price;
        sellerProduct.Stockquantity = sellerProductDto.Stockquantity;

        await _sellerProductRepository.UpdateAsync(sellerProduct);

        return sellerProductDto;
    }
    
    public async Task UpdateRangeAsync(List<Sellerproduct> entities)
    {
        await _sellerProductRepository.UpdateRangeAsync(entities);
    }
    
    public async Task<Unit> DeleteAsync(int id)
    {
        var sellerProduct = await _sellerProductRepository.GetByIdAsync(id);
        if (sellerProduct != null)
        {
            await _sellerProductRepository.DeleteAsync(sellerProduct);
        }
        return Unit.Value;
    }
}