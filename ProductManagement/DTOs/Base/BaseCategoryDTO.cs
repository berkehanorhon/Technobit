namespace ProductManagement.DTOs.Base;

public class BaseCategoryDTO
{
    public string Name { get; set; } = null!;

    public int? Subcategoryid { get; set; }

    public string? Description { get; set; }
}