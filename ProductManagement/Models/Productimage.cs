using System;
using System.Collections.Generic;

namespace ProductManagement.Models;

public partial class Productimage
{
    public int Id { get; set; }

    public int Productid { get; set; }

    public string? Imagepath { get; set; }

    public virtual Product Product { get; set; } = null!;
}
