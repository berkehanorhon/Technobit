using System;
using System.Collections.Generic;

namespace ProductManagement.Models;

public partial class Product
{
    public int Id { get; set; }

    public int Categoryid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Productdetailtag> Productdetailtags { get; set; } = new List<Productdetailtag>();

    public virtual ICollection<Productimage> Productimages { get; set; } = new List<Productimage>();

    public virtual ICollection<Sellerproduct> Sellerproducts { get; set; } = new List<Sellerproduct>();
}
