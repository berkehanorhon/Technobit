using System;
using System.Collections.Generic;

namespace ProductManagement.Models;

public partial class Seller
{
    public int Userid { get; set; }

    public string Name { get; set; } = null!;

    public string? Avatarpath { get; set; }

    public string? Wallpaperpath { get; set; }

    public bool? Isvalidated { get; set; }

    public virtual ICollection<Sellerproduct> Sellerproducts { get; set; } = new List<Sellerproduct>();

    public virtual User User { get; set; } = null!;
}
