using ProductManagement.Models;

namespace ProductManagement.DTOs.Base;

public class BaseProductImageDTO
{
    public int Productid { get; set; }

    public string? Imagepath { get; set; }
    
}