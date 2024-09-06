using System.ComponentModel.DataAnnotations;

namespace TechnoBit.Models;

public class User : BaseEntity
{
    [MaxLength(50)]
    public string Username { get; set; }
    [MaxLength(50)]
    public string PasswordHash { get; set; }
    [MaxLength(50)]
    public string? RefreshToken { get; set; }
    [MaxLength(50)]
    public string Email { get; set; }
    public DateTimeOffset? RefreshTokenExpiryTime { get; set; }
}
