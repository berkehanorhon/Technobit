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
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(ITokenService tokenService, IUserRepository userRepository, IConfiguration configuration)
    {
        _tokenService = tokenService;
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task<TokenModel> Login(LoginModel model)
    {
        var user = await _userRepository.GetUserByUsernameAsync(model.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Geçersiz kullanıcı adı veya şifre.");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        
        var returnModel = new TokenModel();
        
        returnModel.AccessToken = _tokenService.GenerateAccessToken(claims);
        returnModel.RefreshToken = _tokenService.GenerateRefreshToken();
        
        user.RefreshToken = returnModel.RefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["JwtSettings:RefreshTokenExpirationDays"]));
        await _userRepository.UpdateUserAsync(user);

        return returnModel;
    }

    public async Task Register(RegisterModel model)
    {
        if (await _userRepository.GetUserByUsernameAsync(model.Username) != null || await _userRepository.GetUserByEmailAsync(model.Email) != null)
        {
            throw new Exception("Kullanıcı adı veya e-posta zaten mevcut.");
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
        
        // TODO fonksiyonda bir hata kontrolü vs boolean dönmesi lazım mı?
        
        await _userRepository.AddUserAsync(new User
        {
            Username = model.Username,
            PasswordHash = passwordHash,
            Email = model.Email
        });
        
    }

    public async Task<TokenModel> RefreshToken(TokenModel model)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(model.AccessToken);
        var username = principal.Identity.Name;

        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user == null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            throw new UnauthorizedAccessException("Geçersiz veya süresi dolmuş refresh token.");
        
        TokenModel returnModel = new TokenModel();
        
        returnModel.AccessToken = _tokenService.GenerateAccessToken(principal.Claims);
        returnModel.RefreshToken = _tokenService.GenerateRefreshToken();
        
        user.RefreshToken = returnModel.RefreshToken;
        await _userRepository.UpdateUserAsync(user);

        return returnModel;
    }

    public async Task RevokeToken(RevokeTokenModel model)
    {
        // TODO çok kötü bir yaklaşlım bunu değiştirelim

        var user = await _userRepository.GetUserByTokenAsync(model.RefreshToken);

        if (user == null)
            throw new Exception("Geçersiz token.");

        user.RefreshToken = null;
        await _userRepository.UpdateUserAsync(user);
    }
}
