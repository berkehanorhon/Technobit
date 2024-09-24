using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProductManagement.Interfaces;

namespace ProductManagement.Extensions;
public class ExcelReader : IExcelReader
{
    public async Task<List<List<string>>> ReadAllAsync(IFormFile file)
    {
        var data = new List<List<string>>();

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            stream.Position = 0; // Akışı başa al

            using (var package = new ExcelPackage(stream))
            {
                foreach (var worksheet in package.Workbook.Worksheets)
                {
                    for (int row = 1; row <= worksheet.Dimension.End.Row; row++)
                    {
                        var rowData = new List<string>();
                        for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                        {
                            rowData.Add(worksheet.Cells[row, col].Text);
                        }
                        data.Add(rowData);
                    }
                }
            }
        }

        return data;
    }
}