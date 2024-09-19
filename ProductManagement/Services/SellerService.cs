using MediatR;
using ProductManagement.DTOs.Create;
using ProductManagement.DTOs.Read;
using ProductManagement.DTOs.Update;
using ProductManagement.Interfaces;
using ProductManagement.Models;

namespace ProductManagement.Services;


public class SellerService : ISellerService
{
    private readonly IRepository<Seller> _sellerRepository;

    public SellerService(IRepository<Seller> sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<IEnumerable<SellerDTO>> GetAllAsync()
    {
        var categories = await _sellerRepository.GetAllAsync();
        return categories.Select(p => new SellerDTO
        {
            Id = p.Id,
            Name = p.Name,
            Avatarpath = p.Avatarpath,
            Wallpaperpath = p.Wallpaperpath
        });
    }

    public async Task<SellerDTO?> GetByIdAsync(int id)
    {
        var seller = await _sellerRepository.GetByIdAsync(id);
        if (seller == null) return null;

        return new SellerDTO
        {
            Id = seller.Id,
            Name = seller.Name,
            Avatarpath = seller.Avatarpath,
            Wallpaperpath = seller.Wallpaperpath,
        };
    }

    public async Task<int> CreateAsync(CreateSellerDTO createSellerDto)
    {
        var seller = new Seller
        {
            Id = createSellerDto.Id,
            Name = createSellerDto.Name,
            Avatarpath = createSellerDto.Avatarpath,
            Wallpaperpath = createSellerDto.Wallpaperpath
        };

        await _sellerRepository.AddAsync(seller);

        return seller.Id;
    }

    public async Task<UpdateSellerDTO> UpdateAsync(UpdateSellerDTO sellerDto)
    {
        var seller = await _sellerRepository.GetByIdAsync(sellerDto.Id);
        if (seller == null) return null;

        seller.Name = sellerDto.Name;
        seller.Avatarpath = sellerDto.Avatarpath;
        seller.Wallpaperpath = sellerDto.Wallpaperpath;

        await _sellerRepository.UpdateAsync(seller);

        return sellerDto;
    }

    public async Task<Unit> DeleteAsync(int id)
    {
        var seller = await _sellerRepository.GetByIdAsync(id);
        if (seller != null)
        {
            await _sellerRepository.DeleteAsync(seller);
        }
        return Unit.Value;
    }
}