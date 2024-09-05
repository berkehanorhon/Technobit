using Microsoft.AspNetCore.Mvc;
using TechnoBit.Data;
using TechnoBit.DTOs;
using TechnoBit.Models;
using TechnoBit.Services;

namespace TechnoBit.Interfaces;

public interface IAuthController
{
    IActionResult Login([FromBody] LoginDTO dto);
    IActionResult Register(RegisterDTO dto);
    IActionResult Refresh([FromBody] TokenDTO dto);
    IActionResult Revoke([FromBody] RevokeTokenDTO dto);


}