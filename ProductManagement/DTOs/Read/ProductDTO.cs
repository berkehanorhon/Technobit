namespace ProductManagement.DTOs.Read;

public class ProductDTO
{
    public int Id { get; set; }
    public int Categoryid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}