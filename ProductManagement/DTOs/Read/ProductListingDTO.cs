namespace ProductManagement.DTOs.Read;

public class ProductListingDTO
{
    public int count { get; set; }
    public int pageNumber { get; set; }
    public int? categoryId { get; set; }
}