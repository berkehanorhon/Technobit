using System;
using System.Collections.Generic;

namespace ProductManagement.Models;

public partial class Productdetailtag
{
    public int Id { get; set; }

    public int Productid { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
