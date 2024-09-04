using TechnoBit.Models;

// TODO void yerine Task kullanmak doğru mu?

namespace TechnoBit.Interfaces
{
    public interface IAuthService
    {
        Task<TokenModel> Login(LoginModel model);
        Task Register(RegisterModel model);
        Task<TokenModel> RefreshToken(TokenModel model);
        Task RevokeToken(RevokeTokenModel model);
    }
}