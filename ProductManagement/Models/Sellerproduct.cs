using System;
using System.Collections.Generic;

namespace ProductManagement.Models;

public partial class Sellerproduct
{
    public int Productid { get; set; }

    public int Sellerid { get; set; }

    public decimal Price { get; set; }

    public int Stockquantity { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Seller Seller { get; set; } = null!;
}
