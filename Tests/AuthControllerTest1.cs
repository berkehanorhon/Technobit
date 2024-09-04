using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using TechnoBit.Controllers;
using TechnoBit.Interfaces;
using TechnoBit.Models;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.IdentityModel.Tokens;

namespace TechnoBit.Tests
{
    public class AuthControllerTests1
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly AuthController _controller;

        public AuthControllerTests1()
        {
            _authServiceMock = new Mock<IAuthService>();
            _tokenServiceMock = new Mock<ITokenService>();
            _configurationMock = new Mock<IConfiguration>();

            _controller = new AuthController(_authServiceMock.Object, _tokenServiceMock.Object, _configurationMock.Object);
        }

        [Fact]
        public void Login_ValidModel_ReturnsOkResultWithTokens()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Username = "testuser",
                Password = "testpassword"
            };

            var tokens = new TokenModel
            {
                AccessToken = "fakeAccessToken",
                RefreshToken = "fakeRefreshToken"
            };

            _authServiceMock.Setup(service => service.Login(loginModel)).Returns(tokens);

            // Act
            var result = _controller.Login(loginModel) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var response = result.Value as TokenModel;
            Assert.NotNull(response);
            Assert.Equal("fakeAccessToken", response.AccessToken);
            Assert.Equal("fakeRefreshToken", response.RefreshToken);
        }
        
        [Fact]
        public void Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Username = "invaliduser",
                Password = "invalidpassword"
            };

            _authServiceMock.Setup(service => service.Login(loginModel)).Throws(new UnauthorizedAccessException("Invalid credentials"));

            // Act
            var result = _controller.Login(loginModel) as UnauthorizedObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
            Assert.Equal("Invalid credentials", result.Value);
        }
        [Fact]
        public void Login_ThrowsException_ReturnsServerError()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Username = "testuser",
                Password = "testpassword"
            };

            _authServiceMock.Setup(service => service.Login(loginModel)).Throws(new Exception("Unknown error"));
            _configurationMock.Setup(config => config["ErrorMessages::UnknownError"]).Returns("An unknown error occurred");

            // Act
            var result = _controller.Login(loginModel) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("An unknown error occurred", result.Value);
        }
        [Fact]
        public void Register_ValidModel_ReturnsOkResult()
        {
            // Arrange
            var registerModel = new RegisterModel
            {
                Username = "newuser",
                Password = "newpassword"
            };

            // Act
            var result = _controller.Register(registerModel) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("Kayıt başarılı.", result.Value);
        }
        [Fact]
        public void Register_ThrowsException_ReturnsBadRequest()
        {
            // Arrange
            var registerModel = new RegisterModel
            {
                Username = "newuser",
                Password = "newpassword"
            };

            _authServiceMock.Setup(service => service.Register(registerModel)).Throws(new Exception("Registration failed"));

            // Act
            var result = _controller.Register(registerModel) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Registration failed", result.Value);
        }
        [Fact]
        public void Refresh_ValidToken_ReturnsOkResultWithNewTokens()
        {
            // Arrange
            var tokenModel = new TokenModel
            {
                AccessToken = "existingAccessToken",
                RefreshToken = "existingRefreshToken"
            };

            var newTokens = new TokenModel
            {
                AccessToken = "newAccessToken",
                RefreshToken = "newRefreshToken"
            };

            _authServiceMock.Setup(service => service.RefreshToken(tokenModel)).Returns(newTokens);

            // Act
            var result = _controller.Refresh(tokenModel) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var response = result.Value as TokenModel;
            Assert.NotNull(response);
            Assert.Equal("newAccessToken", response.AccessToken);
            Assert.Equal("newRefreshToken", response.RefreshToken);
        }
        [Fact]
        public void Refresh_InvalidToken_ReturnsBadRequest()
        {
            // Arrange
            var tokenModel = new TokenModel
            {
                AccessToken = "invalidAccessToken",
                RefreshToken = "invalidRefreshToken"
            };

            // Mock the configuration to return the expected error message
            _configurationMock.Setup(config => config["ErrorMessages::InvalidTokenError"]).Returns("Geçersiz token.");

            // Simulate the service throwing an exception
            _authServiceMock.Setup(service => service.RefreshToken(tokenModel)).Throws(new SecurityTokenMalformedException("Invalid token"));

            // Act
            var result = _controller.Refresh(tokenModel) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Geçersiz token.", result.Value);
        }
        [Fact]
        public void Refresh_ThrowsException_ReturnsServerError()
        {
            // Arrange
            var tokenModel = new TokenModel
            {
                AccessToken = "existingAccessToken",
                RefreshToken = "existingRefreshToken"
            };

            _authServiceMock.Setup(service => service.RefreshToken(tokenModel)).Throws(new Exception("Unknown error"));
            _configurationMock.Setup(config => config["ErrorMessages::UnknownError"]).Returns("An unknown error occurred");

            // Act
            var result = _controller.Refresh(tokenModel) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("An unknown error occurred", result.Value);
        }
        [Fact]
        public void Revoke_ValidModel_ReturnsNoContent()
        {
            // Arrange
            var revokeTokenModel = new RevokeTokenModel
            {
                RefreshToken = "someToken"
            };

            // Act
            var result = _controller.Revoke(revokeTokenModel) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }
        [Fact]
        public void Revoke_ThrowsException_ReturnsBadRequest()
        {
            // Arrange
            var revokeTokenModel = new RevokeTokenModel
            {
                RefreshToken = "someToken"
            };

            _authServiceMock.Setup(service => service.RevokeToken(revokeTokenModel)).Throws(new Exception("Revoke failed"));

            // Act
            var result = _controller.Revoke(revokeTokenModel) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Revoke failed", result.Value);
        }


    }
}