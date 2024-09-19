using System;
using System.Collections.Generic;

namespace ProductManagement.Models;

public partial class Seller
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Avatarpath { get; set; }

    public string? Wallpaperpath { get; set; }

    public bool? Isvalidated { get; set; }

    public virtual User IdNavigation { get; set; } = null!;

    public virtual ICollection<Sellerproduct> Sellerproducts { get; set; } = new List<Sellerproduct>();
}
