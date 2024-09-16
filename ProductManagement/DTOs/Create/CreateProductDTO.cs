namespace ProductManagement.DTOs.Create;

public class CreateProductDTO
{
    public int Categoryid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}