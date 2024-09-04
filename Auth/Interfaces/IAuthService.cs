using TechnoBit.Models;

namespace TechnoBit.Interfaces
{
    public interface IAuthService
    {
        TokenModel Login(LoginModel model);
        void Register(RegisterModel model);
        TokenModel RefreshToken(TokenModel model);
        void RevokeToken(RevokeTokenModel model);
    }
}