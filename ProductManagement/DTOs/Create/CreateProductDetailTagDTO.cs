﻿namespace ProductManagement.DTOs.Create;

public class CreateProductDetailTagDTO
{
    public int Productid { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;
}