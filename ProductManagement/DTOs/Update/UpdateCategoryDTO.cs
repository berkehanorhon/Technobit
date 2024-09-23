using ProductManagement.DTOs.Base;

namespace ProductManagement.DTOs.Update;

public class UpdateCategoryDTO : BaseCategoryDTO
{
    public int Id { get; set; }
}