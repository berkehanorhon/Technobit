using TechnoBit.DTOs;
using TechnoBit.Models;

// TODO void yerine Task kullanmak doğru mu?

namespace TechnoBit.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDTO> Login(LoginDTO dto);
        Task Register(RegisterDTO dto);
        Task<TokenDTO> RefreshToken(TokenDTO dto);
        Task RevokeToken(RevokeTokenDTO dto);
    }
}