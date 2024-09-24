using System;
using System.Collections.Generic;
using ProductManagement.Interfaces;
using ProductManagement.Models;

namespace ProductManagement.Extensions;
public class ExcelToSellerProduct : IExcelToSellerProduct
{
    public List<Sellerproduct> ConvertToProducts(List<List<string>> excelData)
    {
        var products = new List<Sellerproduct>();

        for (int rowIndex = 0; rowIndex < excelData.Count; rowIndex++)
        {
            var row = excelData[rowIndex];

            if (row.Count >= 4)
            {
                if (!int.TryParse(row[0], out var pid))
                {
                    throw new InvalidDataException($"Invalid Product ID at row {rowIndex + 1}, column 1");
                }

                if (!int.TryParse(row[1], out var sid))
                {
                    throw new InvalidDataException($"Invalid Seller ID at row {rowIndex + 1}, column 2");
                }

                if (!decimal.TryParse(row[2], out var price))
                {
                    throw new InvalidDataException($"Invalid Price at row {rowIndex + 1}, column 3");
                }

                if (!int.TryParse(row[3], out var quantity))
                {
                    throw new InvalidDataException($"Invalid Stock Quantity at row {rowIndex + 1}, column 4");
                }

                products.Add(new Sellerproduct
                {
                    Productid = pid,
                    Sellerid = sid,
                    Price = price,
                    Stockquantity = quantity,
                });
            }
            else
            {
                throw new InvalidDataException($"Missing data at row {rowIndex + 1}, expected at least 4 columns.");
            }
        }

        return products;
    }
}