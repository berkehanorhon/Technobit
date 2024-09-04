using Microsoft.AspNetCore.Mvc;
using TechnoBit.Data;
using TechnoBit.Models;
using TechnoBit.Services;

namespace TechnoBit.Interfaces;

public interface IAuthController
{
    IActionResult Login([FromBody] LoginModel model);
    IActionResult Register(RegisterModel model);
    IActionResult Refresh([FromBody] TokenModel model);
    IActionResult Revoke([FromBody] RevokeTokenModel model);


}