namespace ProductManagement.DTOs.Base;

public abstract class BaseProductDTO
{
    public int Categoryid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}