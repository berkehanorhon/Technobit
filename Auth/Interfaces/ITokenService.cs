using System.Security.Claims;
using TechnoBit.Models;

namespace TechnoBit.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}