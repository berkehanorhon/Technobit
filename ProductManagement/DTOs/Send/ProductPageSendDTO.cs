using ProductManagement.DTOs.Base;

namespace ProductManagement.DTOs.Read;

public class ProductPageSendDTO
{
    public string name { get; set; }
    public string description { get; set; }
    public decimal price { get; set; }
    public int quantity { get; set; }
    public List<BaseCategoryDTO> categories { get; set; }
    public List<BaseProductDetailTagDTO> tags { get; set; }
    public List<string>? images { get; set; } = new List<string>();
    public string sellerName { get; set; }
    public int sellerID { get; set; }
}