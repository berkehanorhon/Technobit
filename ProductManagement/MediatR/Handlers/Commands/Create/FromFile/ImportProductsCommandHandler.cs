using MediatR;
using ProductManagement.Interfaces;
using ProductManagement.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProductManagement.MediatR.Commands.Create.FromFile;

namespace ProductManagement.MediatR.Handlers.Commands.Create.FromFile
{
    public class ImportProductsCommandHandler : IRequestHandler<ImportProductsCommand, bool>
    {
        private readonly IExcelReader _excelReader;
        private readonly IExcelToSellerProduct _excelToSellerProduct;
        private readonly ISellerProductService _sellerProductService;

        public ImportProductsCommandHandler(
            IExcelReader excelReader,
            IExcelToSellerProduct excelToSellerProduct,
            ISellerProductService sellerProductService)
        {
            _excelReader = excelReader;
            _excelToSellerProduct = excelToSellerProduct;
            _sellerProductService = sellerProductService;
        }

        public async Task<bool> Handle(ImportProductsCommand request, CancellationToken cancellationToken)
        {
            // Excel dosyasını oku
            var excelData = await _excelReader.ReadAllAsync(request.ExcelFile);

            // Excel'deki verileri Product listesine çevir
            var products = _excelToSellerProduct.ConvertToProducts(excelData);

            // Veritabanına kayıt işlemi
            try
            {
                await _sellerProductService.UpdateRangeAsync(products);

                // ID kontrolü yap
                var updatedProductIds = products.Select(p => p.Id).ToList(); // Güncellenen ürünlerin ID'lerini al
                var successfulUpdates = updatedProductIds.All(id => id > 0); // ID'ler 0'dan büyükse güncelleme başarılı sayılır

                return successfulUpdates; // Eğer tüm ID'ler başarılıysa true, aksi takdirde false dön
            }
            catch
            {
                // Hata durumunda false dön
                return false;
            }
            
        }
    }
}