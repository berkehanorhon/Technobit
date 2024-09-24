using ProductManagement.Models;
using System.Collections.Generic;

namespace ProductManagement.Interfaces
{
    public interface IExcelToSellerProduct
    {
        List<Sellerproduct> ConvertToProducts(List<List<string>> excelData);
    }
}