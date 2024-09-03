using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using TechnoBit.Data;
using TechnoBit.Services;
using TechnoBit.Models;
namespace TechnoBit.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly TokenService _tokenService;
    private readonly IConfiguration _configuration;

    public AuthController(ApplicationDbContext context, TokenService tokenService, IConfiguration configuration)
    {
        _context = context;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == model.Username);
        
        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
        {
            return Unauthorized("Geçersiz kullanıcı adı veya şifre.");
        }
        
        if (user == null)
            return Unauthorized();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var accessToken = _tokenService.GenerateAccessToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["JwtSettings:RefreshTokenExpirationDays"]));

        _context.SaveChanges();

        return Ok(new
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }
    
    [HttpPost("register")]
    public IActionResult Register(RegisterModel model)
    {
        // Kullanıcı adı veya e-posta zaten var mı kontrol et
        if (_context.Users.Any(u => u.Username == model.Username || u.Email == model.Email))
        {
            return BadRequest("Kullanıcı adı veya e-posta zaten mevcut.");
        }

        // Şifreyi hashle
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

        // Yeni kullanıcı oluştur
        var user = new User
        {
            Username = model.Username,
            PasswordHash = passwordHash,
            Email = model.Email
        };

        // Kullanıcıyı veritabanına ekle
        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok("Kayıt başarılı.");
    }

    [HttpPost("refresh")]
    public IActionResult Refresh([FromBody] TokenModel model)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(model.AccessToken);
        var username = principal.Identity.Name;

        var user = _context.Users.SingleOrDefault(u => u.Username == username);

        if (user == null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return Unauthorized();

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        _context.SaveChanges();

        return Ok(new
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }
    
    [HttpPost("revoke")]
    public IActionResult Revoke([FromBody] RevokeTokenModel model)
    {
        var user = _context.Users.SingleOrDefault(u => u.RefreshToken == model.RefreshToken);

        if (user == null)
            return BadRequest("Invalid token");

        user.RefreshToken = null;
        _context.SaveChanges();

        return NoContent();
    }

}
