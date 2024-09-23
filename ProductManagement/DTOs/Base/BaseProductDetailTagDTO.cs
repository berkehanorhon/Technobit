namespace ProductManagement.DTOs.Base;

public class BaseProductDetailTagDTO
{
    public int Productid { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;
}