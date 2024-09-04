using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TechnoBit.Controllers;
using TechnoBit.Interfaces;
using TechnoBit.Models;
using Xunit;

namespace TechnoBit.Tests
{
    public class AuthControllerTests
    {
        private readonly AuthController _controller;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<IConfiguration> _configurationMock;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(c => c["ErrorMessages:InvalidTokenError"]).Returns("Geçersiz token.");
            _configurationMock.Setup(c => c["ErrorMessages:UnknownError"]).Returns("Bilinmeyen bir hata oluştu.");
            _controller = new AuthController(_authServiceMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task Login_ReturnsOk_WhenLoginIsSuccessful()
        {
            // Arrange
            var loginModel = new LoginModel { Username = "test", Password = "password" };
            var token = new TokenModel { AccessToken = "accessToken", RefreshToken = "refreshToken" };
            _authServiceMock.Setup(s => s.Login(loginModel)).ReturnsAsync(token);

            // Act
            var result = await _controller.Login(loginModel) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(token, result.Value);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenUnauthorizedAccessExceptionIsThrown()
        {
            // Arrange
            var loginModel = new LoginModel { Username = "test", Password = "password" };
            _authServiceMock.Setup(s => s.Login(loginModel)).ThrowsAsync(new UnauthorizedAccessException("Unauthorized"));

            // Act
            var result = await _controller.Login(loginModel) as UnauthorizedObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
            Assert.Equal("Unauthorized", result.Value);
        }

        [Fact]
        public async Task Register_ReturnsOk_WhenRegisterIsSuccessful()
        {
            // Arrange
            var registerModel = new RegisterModel { Username = "newUser", Password = "password", Email = "new@example.com" };
            _authServiceMock.Setup(s => s.Register(registerModel)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Register(registerModel) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("Kayıt başarılı.", result.Value);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var registerModel = new RegisterModel { Username = "newUser", Password = "password", Email = "new@example.com" };
            _authServiceMock.Setup(s => s.Register(registerModel)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.Register(registerModel) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Error", result.Value);
        }

        [Fact]
        public async Task Refresh_ReturnsOk_WhenRefreshTokenIsSuccessful()
        {
            // Arrange
            var tokenModel = new TokenModel { RefreshToken = "oldToken" };
            var newToken = new TokenModel { AccessToken = "accessToken", RefreshToken = "refreshToken" };
            _authServiceMock.Setup(s => s.RefreshToken(tokenModel)).ReturnsAsync(newToken);

            // Act
            var result = await _controller.Refresh(tokenModel) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(newToken, result.Value);
        }

        [Fact]
        public async Task Refresh_ReturnsBadRequest_WhenSecurityTokenMalformedExceptionIsThrown()
        {
            // Arrange
            var tokenModel = new TokenModel { RefreshToken = "oldToken" };
            _authServiceMock.Setup(s => s.RefreshToken(tokenModel)).ThrowsAsync(new SecurityTokenMalformedException("Invalid token"));

            // Act
            var result = await _controller.Refresh(tokenModel) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Geçersiz token.", result.Value);
        }

        [Fact]
        public async Task Revoke_ReturnsNoContent_WhenRevokeIsSuccessful()
        {
            // Arrange
            var revokeModel = new RevokeTokenModel { RefreshToken = "token" };
            _authServiceMock.Setup(s => s.RevokeToken(revokeModel)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Revoke(revokeModel) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async Task Revoke_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var revokeModel = new RevokeTokenModel { RefreshToken = "token" };
            _authServiceMock.Setup(s => s.RevokeToken(revokeModel)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.Revoke(revokeModel) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Error", result.Value);
        }
    }
}
