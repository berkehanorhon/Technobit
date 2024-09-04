using TechnoBit.Models;

namespace TechnoBit.Interfaces
{
    public interface IAuthService
    {
        (string AccessToken, string RefreshToken) Login(LoginModel model);
        void Register(RegisterModel model);
        (string AccessToken, string RefreshToken) RefreshToken(TokenModel model);
        void RevokeToken(RevokeTokenModel model);
    }
}