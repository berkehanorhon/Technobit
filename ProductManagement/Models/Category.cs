using System;
using System.Collections.Generic;

namespace ProductManagement.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Subcategoryid { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Category> InverseSubcategory { get; set; } = new List<Category>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual Category? Subcategory { get; set; }
}
