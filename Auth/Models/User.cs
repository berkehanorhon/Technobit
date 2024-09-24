using System;
using System.Collections.Generic;

namespace TechnoBit.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public string Email { get; set; } = null!;

    public DateTime? RefreshTokenExpiryTime { get; set; }

    public bool IsAdmin { get; set; }

    public virtual Seller? Seller { get; set; }
}
