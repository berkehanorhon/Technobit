using System.ComponentModel.DataAnnotations;

namespace TechnoBit.DTOs;

public class RevokeTokenDTO
{
    [Required]
    public string RefreshToken { get; set; }
}