using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using BCrypt.Net;
using TechnoBit.Data;
using TechnoBit.DTOs;
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

    public async Task<TokenDTO> Login(LoginDTO dto)
    {
        var user = await _userRepository.GetUserByUsernameAsync(dto.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Geçersiz kullanıcı adı veya şifre.");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        
        var returnModel = new TokenDTO();
        
        returnModel.AccessToken = _tokenService.GenerateAccessToken(claims);
        returnModel.RefreshToken = _tokenService.GenerateRefreshToken();
        
        user.RefreshToken = returnModel.RefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["JwtSettings:RefreshTokenExpirationDays"]));
        await _userRepository.UpdateModelAsync(user);

        return returnModel;
    }

    public async Task Register(RegisterDTO dto)
    {
        if (await _userRepository.GetUserByUsernameAsync(dto.Username) != null || await _userRepository.GetUserByEmailAsync(dto.Email) != null)
        {
            throw new Exception("Kullanıcı adı veya e-posta zaten mevcut.");
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        
        // TODO fonksiyonda bir hata kontrolü vs boolean dönmesi lazım mı?
        
        await _userRepository.AddModelAsync(new User
        {
            Username = dto.Username,
            PasswordHash = passwordHash,
            Email = dto.Email
        });
        
    }

    public async Task<TokenDTO> RefreshToken(TokenDTO dto)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(dto.AccessToken);
        var username = principal.Identity.Name;

        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user == null || user.RefreshToken != dto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            throw new UnauthorizedAccessException("Geçersiz veya süresi dolmuş refresh token.");
        
        TokenDTO returnDto = new TokenDTO();
        
        returnDto.AccessToken = _tokenService.GenerateAccessToken(principal.Claims);
        returnDto.RefreshToken = _tokenService.GenerateRefreshToken();
        
        user.RefreshToken = returnDto.RefreshToken;
        await _userRepository.UpdateModelAsync(user);

        return returnDto;
    }

    public async Task RevokeToken(RevokeTokenDTO dto)
    {
        // TODO çok kötü bir yaklaşlım bunu değiştirelim

        var user = await _userRepository.GetUserByTokenAsync(dto.RefreshToken);

        if (user == null)
            throw new Exception("Geçersiz token.");

        user.RefreshToken = null;
        await _userRepository.UpdateModelAsync(user);
    }
}
