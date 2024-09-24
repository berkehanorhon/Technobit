using MediatR;
using Microsoft.AspNetCore.Http;
using ProductManagement.Models;

namespace ProductManagement.MediatR.Commands.Create.FromFile
{
    public class ImportProductsCommand : IRequest<bool>
    {
        public IFormFile ExcelFile { get; }

        public ImportProductsCommand(IFormFile excelFile)
        {
            ExcelFile = excelFile;
        }
    }
}