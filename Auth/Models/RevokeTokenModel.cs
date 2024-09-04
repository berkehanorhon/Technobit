using System.ComponentModel.DataAnnotations;

namespace TechnoBit.Models;

public class RevokeTokenModel
{
    [Required]
    public string RefreshToken { get; set; }
}