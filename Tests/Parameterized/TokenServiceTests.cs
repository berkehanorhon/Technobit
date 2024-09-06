using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using TechnoBit.Services;
using Xunit;

public class TokenServiceTests
{
    private readonly Mock<IConfiguration> _mockConfiguration;

    public TokenServiceTests()
    {
        _mockConfiguration = new Mock<IConfiguration>();
    }

    [Theory]
    [InlineData("claim_value", "issuer", "audience", 60, "super_se321321321321321321321321cret_key")]
    public void GenerateAccessToken_ShouldReturnToken(string claimValue, string issuer, string audience, double expirationMinutes, string secretKey)
    {
        // Arrange
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, claimValue) };

        var mockSection = new Mock<IConfigurationSection>();
        mockSection.Setup(x => x["Secret"]).Returns(secretKey);
        mockSection.Setup(x => x["Issuer"]).Returns(issuer);
        mockSection.Setup(x => x["Audience"]).Returns(audience);
        mockSection.Setup(x => x["AccessTokenExpirationMinutes"]).Returns(expirationMinutes.ToString());

        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration.Setup(x => x.GetSection("JwtSettings")).Returns(mockSection.Object);

        var tokenService = new TokenService(mockConfiguration.Object);

        // Act
        var token = tokenService.GenerateAccessToken(claims);

        // Assert
        Assert.NotNull(token);
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
        Assert.NotNull(jwtToken);

        Assert.Equal(issuer, jwtToken.Issuer);
        Assert.Contains(audience, jwtToken.Audiences);
        Assert.True(jwtToken.ValidTo > DateTime.UtcNow);
        Assert.True(jwtToken.ValidFrom <= DateTime.UtcNow);
    }
    
    [Fact]
    public void GenerateRefreshToken_ShouldReturnToken()
    {
        // Arrange
        var tokenService = new TokenService(_mockConfiguration.Object);

        // Act
        var token = tokenService.GenerateRefreshToken();

        // Assert
        Assert.NotNull(token);
        Assert.Equal(32, token.Length); // Genellikle 32 karakter uzunluğunda bir GUID beklenir
    }
    
    [Theory]
    [InlineData("valid_token", true)]  // Bu token geçerli bir token olmalıdır
    [InlineData("invalid_token", false)] // Bu token geçersiz bir token olmalıdır
    public void GetPrincipalFromExpiredToken_ShouldReturnClaimsPrincipal(string token, bool isValid)
    {
        // Arrange
        var jwtSettings = new Dictionary<string, string>
        {
            { "Secret", "super_secret_key" },
            { "Issuer", "issuer" },
            { "Audience", "audience" }
        };

        _mockConfiguration.Setup(c => c["JwtSettings:Secret"]).Returns(jwtSettings["Secret"]);
        _mockConfiguration.Setup(c => c["JwtSettings:Issuer"]).Returns(jwtSettings["Issuer"]);
        _mockConfiguration.Setup(c => c["JwtSettings:Audience"]).Returns(jwtSettings["Audience"]);

        var tokenService = new TokenService(_mockConfiguration.Object);

        // Act
        ClaimsPrincipal? principal;
        if (isValid)
        {
            // Generate a valid token for testing
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, "test_user") };
            var validToken = tokenService.GenerateAccessToken(claims);
            principal = tokenService.GetPrincipalFromExpiredToken(validToken);
        }
        else
        {
            principal = tokenService.GetPrincipalFromExpiredToken(token);
        }

        // Assert
        if (isValid)
        {
            Assert.NotNull(principal);
            Assert.True(principal.Identity.IsAuthenticated);
        }
        else
        {
            Assert.Null(principal);
        }
    }


}