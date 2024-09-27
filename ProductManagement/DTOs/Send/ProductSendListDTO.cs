namespace ProductManagement.DTOs.Read;

public class ProductSendListDTO
{
    public int Id { get; set; }
    public int categoryId { get; set; }
    public string name { get; set; }
    public List<string>? images { get; set; } = new List<string>();
    public ProductListBestProduct? bestProduct { get; set; }
}