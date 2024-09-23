namespace ProductManagement.DTOs.Base;

public class BaseSellerProductDTO
{
    public int Productid { get; set; }

    public int Sellerid { get; set; }

    public decimal Price { get; set; }

    public int Stockquantity { get; set; }
}