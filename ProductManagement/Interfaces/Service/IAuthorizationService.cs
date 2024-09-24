using System.Security.Claims;

namespace ProductManagement.Interfaces;

public interface IAuthorizationService
{
    Task<bool> ValidateProductOwnerAsync(int productId, ClaimsPrincipal user);
}