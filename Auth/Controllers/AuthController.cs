using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TechnoBit.DTOs;
using TechnoBit.Interfaces;
using TechnoBit.Models;

namespace TechnoBit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {
                return Ok(await _authService.Login(dto));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                // Genel hata yönetimi
                return StatusCode(500, _configuration["ErrorMessages:UnknownError"]);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            try
            {
                await _authService.Register(dto);
                return Ok("Kayıt başarılı.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDTO dto)
        {
            try
            {
                return Ok(await _authService.RefreshToken(dto));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (SecurityTokenMalformedException ex)
            {
                return BadRequest(_configuration["ErrorMessages:InvalidTokenError"]);
            }
            catch (SecurityTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500,_configuration["ErrorMessages:UnknownError"]);
            }
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke([FromBody] RevokeTokenDTO dto)
        {
            try
            {
                await _authService.RevokeToken(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
