using ProductManagement.DTOs.Base;

namespace ProductManagement.DTOs.Update;

public class UpdateSellerDTO : BaseSellerDTO
{
    public bool? Isvalidated { get; set; }
}