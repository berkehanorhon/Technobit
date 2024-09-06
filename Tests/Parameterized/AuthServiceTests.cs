using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Moq;
using TechnoBit.DTOs;
using TechnoBit.Interfaces;
using TechnoBit.MediatR.Commands;
using TechnoBit.Models;
using Xunit;

public class AuthServiceTests
{
    private readonly Mock<ITokenService> _mockTokenService;
    private readonly Mock<IMediator> _mockMediator;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _mockTokenService = new Mock<ITokenService>();
        _mockMediator = new Mock<IMediator>();
        _mockConfiguration = new Mock<IConfiguration>();
        _authService = new AuthService(_mockTokenService.Object, _mockMediator.Object, _mockConfiguration.Object);
    }

    [Theory]
    [InlineData("validUsername", "validPassword", "access_token", "refresh_token")]
    [InlineData("anotherUsername", "anotherPassword", "new_access_token", "new_refresh_token")]
    public async Task Login_ShouldReturnTokenDTO_WhenCredentialsAreValid(string username, string password, string accessToken, string refreshToken)
    {
        // Arrange
        var dto = new LoginDTO { Username = username, Password = password };
        var tokenDto = new TokenDTO { AccessToken = accessToken, RefreshToken = refreshToken };
        _mockMediator.Setup(m => m.Send(It.IsAny<LoginCommand>(), default)).ReturnsAsync(tokenDto);

        // Act
        var result = await _authService.Login(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(accessToken, result.AccessToken);
        Assert.Equal(refreshToken, result.RefreshToken);
    }

    [Theory]
    [InlineData("username", "password", "email@example.com")]
    [InlineData("newuser", "newpassword", "newemail@example.com")]
    public async Task Register_ShouldCallMediatorSend_WhenValidRequest(string username, string password, string email)
    {
        // Arrange
        var dto = new RegisterDTO { Username = username, Password = password, Email = email };

        // Act
        await _authService.Register(dto);

        // Assert
        _mockMediator.Verify(m => m.Send(It.Is<RegisterCommand>(c =>
            c.Username == username &&
            c.Password == password &&
            c.Email == email), default), Times.Once);
    }

    [Theory]
    [InlineData("access_token", "refresh_token", "new_access_token", "new_refresh_token")]
    [InlineData("old_access_token", "old_refresh_token", "another_new_access_token", "another_new_refresh_token")]
    public async Task RefreshToken_ShouldReturnTokenDTO_WhenValidToken(string accessToken, string refreshToken, string newAccessToken, string newRefreshToken)
    {
        // Arrange
        var dto = new TokenDTO { AccessToken = accessToken, RefreshToken = refreshToken };
        var tokenDto = new TokenDTO { AccessToken = newAccessToken, RefreshToken = newRefreshToken };
        _mockMediator.Setup(m => m.Send(It.IsAny<RefreshTokenCommand>(), default)).ReturnsAsync(tokenDto);

        // Act
        var result = await _authService.RefreshToken(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newAccessToken, result.AccessToken);
        Assert.Equal(newRefreshToken, result.RefreshToken);
    }

    [Theory]
    [InlineData("refresh_token_1")]
    [InlineData("refresh_token_2")]
    public async Task RevokeToken_ShouldCallMediatorSend_WhenValidRequest(string refreshToken)
    {
        // Arrange
        var dto = new RevokeTokenDTO { RefreshToken = refreshToken };

        // Act
        await _authService.RevokeToken(dto);

        // Assert
        _mockMediator.Verify(m => m.Send(It.Is<RevokeTokenCommand>(c =>
            c.RefreshToken == refreshToken), default), Times.Once);
    }
}
