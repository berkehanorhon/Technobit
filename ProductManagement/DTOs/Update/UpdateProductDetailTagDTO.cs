namespace ProductManagement.DTOs.Update;

public class UpdateProductDetailTagDTO
{
    public int Id { get; set; }

    public int Productid { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;   
}