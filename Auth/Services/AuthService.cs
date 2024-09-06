using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Xml;
using Microsoft.Extensions.Configuration;
using BCrypt.Net;
using MediatR;
using TechnoBit.Data;
using TechnoBit.DTOs;
using TechnoBit.Interfaces;
using TechnoBit.MediatR.Commands;
using TechnoBit.Models;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;

    public AuthService(ITokenService tokenService, IMediator mediator, IConfiguration configuration)
    {
        _tokenService = tokenService;
        _configuration = configuration;
        _mediator = mediator;
    }

    public async Task<TokenDTO> Login(LoginDTO dto)
    {
        var command = new LoginCommand(dto.Username, dto.Password);
        var result = await _mediator.Send(command);
        return result;
    }

    public async Task Register(RegisterDTO dto)
    {
        var command = new RegisterCommand(dto.Username, dto.Password, dto.Email);
        await _mediator.Send(command);

    }

    public async Task<TokenDTO> RefreshToken(TokenDTO dto)
    {
        var command = new RefreshTokenCommand(dto.AccessToken, dto.RefreshToken);
        var result = await _mediator.Send(command);
        return result;
    }

    public async Task RevokeToken(RevokeTokenDTO dto)
    {
        // TODO çok kötü bir yaklaşlım bunu değiştirelim

        var command = new RevokeTokenCommand(dto.RefreshToken);
        await _mediator.Send(command);

    }
}
