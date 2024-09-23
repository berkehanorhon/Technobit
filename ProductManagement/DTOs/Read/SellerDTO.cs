using ProductManagement.DTOs.Base;

namespace ProductManagement.DTOs.Read;

public class SellerDTO : BaseSellerDTO
{
    public bool? Isvalidated { get; set; }
}