using MediatR;
using ProductManagement.DTOs.Create;
using ProductManagement.DTOs.Read;
using ProductManagement.DTOs.Update;
using ProductManagement.Interfaces;
using ProductManagement.Models;

namespace ProductManagement.Services;


public class ProductDetailTagService : IProductDetailTagService
{
    private readonly IRepository<Productdetailtag> _pdtRepository;

    public ProductDetailTagService(IRepository<Productdetailtag> pdtRepository)
    {
        _pdtRepository = pdtRepository;
    }

    public async Task<IEnumerable<ProductDetailTagDTO>> GetAllAsync()
    {
        var pdts = await _pdtRepository.GetAllAsync();
        return pdts.Select(p => new ProductDetailTagDTO
        {
            Id = p.Id,
            Productid = p.Productid,
            Title = p.Title,
            Description = p.Description
        });
    }

    public async Task<ProductDetailTagDTO?> GetByIdAsync(int id)
    {
        var pdt = await _pdtRepository.GetByIdAsync(id);
        if (pdt == null) return null;

        return new ProductDetailTagDTO
        {
            Id = pdt.Id,
            Productid = pdt.Productid,
            Title = pdt.Title,
            Description = pdt.Description
        };
    }

    public async Task<int> CreateAsync(CreateProductDetailTagDTO createPDT)
    {
        var pdt = new Productdetailtag
        {
            Productid = createPDT.Productid,
            Title = createPDT.Title,
            Description = createPDT.Description
        };

        await _pdtRepository.AddAsync(pdt);

        return pdt.Id;
    }

    public async Task<UpdateProductDetailTagDTO> UpdateAsync(UpdateProductDetailTagDTO updt)
    {
        var pdt = await _pdtRepository.GetByIdAsync(updt.Id);
        if (pdt == null) return null;

        pdt.Productid = updt.Productid;
        pdt.Title = updt.Title;
        pdt.Description = updt.Description;

        await _pdtRepository.UpdateAsync(pdt);

        return updt;
    }

    public async Task<Unit> DeleteAsync(int id)
    {
        var pdt = await _pdtRepository.GetByIdAsync(id);
        if (pdt != null)
        {
            await _pdtRepository.DeleteAsync(pdt);
        }
        return Unit.Value;
    }
}