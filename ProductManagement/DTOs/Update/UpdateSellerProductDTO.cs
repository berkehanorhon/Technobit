using ProductManagement.DTOs.Base;

namespace ProductManagement.DTOs.Update;

public class UpdateSellerProductDTO : BaseSellerProductDTO
{
    public int Id { get; set; }
}