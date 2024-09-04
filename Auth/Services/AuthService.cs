using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using BCrypt.Net;
using TechnoBit.Data;
using TechnoBit.Interfaces;
using TechnoBit.Models;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public AuthService(ApplicationDbContext context, ITokenService tokenService, IConfiguration configuration)
    {
        _context = context;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    public (string AccessToken, string RefreshToken) Login(LoginModel model)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == model.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Geçersiz kullanıcı adı veya şifre.");
        }

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

        return (AccessToken: accessToken, RefreshToken: refreshToken);
    }

    public void Register(RegisterModel model)
    {
        if (_context.Users.Any(u => u.Username == model.Username || u.Email == model.Email))
        {
            throw new Exception("Kullanıcı adı veya e-posta zaten mevcut.");
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

        var user = new User
        {
            Username = model.Username,
            PasswordHash = passwordHash,
            Email = model.Email
        };

        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public (string AccessToken, string RefreshToken) RefreshToken(TokenModel model)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(model.AccessToken);
        var username = principal.Identity.Name;

        var user = _context.Users.SingleOrDefault(u => u.Username == username);

        if (user == null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            throw new UnauthorizedAccessException("Geçersiz veya süresi dolmuş refresh token.");

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        _context.SaveChanges();

        return (AccessToken: newAccessToken, RefreshToken: newRefreshToken);
    }

    public void RevokeToken(RevokeTokenModel model)
    {
        var user = _context.Users.SingleOrDefault(u => u.RefreshToken == model.RefreshToken);

        if (user == null)
            throw new Exception("Geçersiz token.");

        user.RefreshToken = null;
        _context.SaveChanges();
    }
}
