using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TechnoBit.Interfaces;
using TechnoBit.Models;

namespace TechnoBit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, ITokenService tokenService, IConfiguration configuration)
        {
            _authService = authService;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            try
            {
                var tokens = _authService.Login(model);
                return Ok(new
                {
                    AccessToken = tokens.AccessToken,
                    RefreshToken = tokens.RefreshToken
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                // Genel hata yönetimi
                return StatusCode(500, _configuration["ErrorMessages::UnknownError"]);
            }
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterModel model)
        {
            try
            {
                _authService.Register(model);
                return Ok("Kayıt başarılı.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] TokenModel model)
        {
            try
            {
                var tokens = _authService.RefreshToken(model);
                return Ok(new
                {
                    AccessToken = tokens.AccessToken,
                    RefreshToken = tokens.RefreshToken
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (SecurityTokenMalformedException ex)
            {
                return BadRequest(_configuration["ErrorMessages::InvalidTokenError"]);
            }
            catch (SecurityTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500,_configuration["ErrorMessages::UnknownError"]);
            }
        }

        [HttpPost("revoke")]
        public IActionResult Revoke([FromBody] RevokeTokenModel model)
        {
            try
            {
                _authService.RevokeToken(model);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
