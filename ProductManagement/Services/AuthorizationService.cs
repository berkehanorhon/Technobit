using System.Security.Claims;
using ProductManagement.Interfaces;
using ProductManagement.Models;

namespace ProductManagement.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly IRepository<Sellerproduct> _sellerProductRepository;
    private readonly IRepository<Seller> _sellerRepository;

    public AuthorizationService(IRepository<Sellerproduct> sellerProductRepository, IRepository<Seller> sellerRepository)
    {
        _sellerProductRepository = sellerProductRepository;
        _sellerRepository = sellerRepository;
    }

    public async Task<bool> ValidateProductOwnerAsync(int productId, ClaimsPrincipal user)
    {
        var product = await _sellerProductRepository.GetByIdAsync(productId);
        if (product == null)
        {
            return false;
        }

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (product.Sellerid.ToString() != userId)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> ValidateUserIsASellerAsync(int sellerId, ClaimsPrincipal user)
    {
        var seller = await _sellerRepository.GetByIdAsync(sellerId);
        return seller != null;
    }
}
