using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProductManagement.Interfaces
{
    public interface IExcelReader
    {
        Task<List<List<string>>> ReadAllAsync(IFormFile file);
    }
}