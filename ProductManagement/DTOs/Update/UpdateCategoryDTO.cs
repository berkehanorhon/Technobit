namespace ProductManagement.DTOs.Update;

public class UpdateCategoryDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Subcategoryid { get; set; }

    public string? Description { get; set; }

}