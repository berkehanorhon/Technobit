using ProductManagement.DTOs.Base;

namespace ProductManagement.DTOs.Update;

public class UpdateProductImageDTO : BaseProductImageDTO
{
    public int Id { get; set; }
}