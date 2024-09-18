namespace ProductManagement.DTOs.Update;

public class UpdateSellerProductDTO
{
    public int Id { get; set; }

    public int Productid { get; set; }

    public int Sellerid { get; set; }

    public decimal Price { get; set; }

    public int Stockquantity { get; set; }
}