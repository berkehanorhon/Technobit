using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.DTOs.Read;
using ProductManagement.Interfaces;
using ProductManagement.Interfaces.Repositories;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly TechnobitpublicContext _context;
        
        public ProductRepository(TechnobitpublicContext context)
            : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));  
        }

        public async Task<(List<ProductSendListDTO> Items, int TotalPages)> GetLatestsWithPaging(ProductListingDTO idto)
        {
            int totalItems = await _context.Products.CountAsync();
            
            int totalPages = (int)Math.Ceiling(totalItems / (double)idto.count);
            
            if (idto.pageNumber < 1)
            {
                idto.pageNumber = 1;
            }
            
            if (idto.pageNumber > totalPages)
            {
                idto.pageNumber = totalPages;
            }
            
            List<Product> items = await _context.Products
                .Where(p => idto.categoryId == null | p.Categoryid == idto.categoryId)
                .OrderByDescending(e => e.Id)
                .Skip((idto.pageNumber - 1) * idto.count)
                .Take(idto.count)
                .ToListAsync();
            
            List<ProductSendListDTO> products = new List<ProductSendListDTO>();
            foreach (Product item in items)
            {
                ProductSendListDTO dto = new ProductSendListDTO
                {
                    Id = item.Id,
                    categoryId = item.Categoryid,
                    name = item.Name,
                    images = await _context.Productimages
                        .Where(pi => pi.Productid == item.Id)
                        .Select(pi => pi.Imagepath)
                        .ToListAsync(),
                    bestProduct = await _context.Sellerproducts
                        .Where(p => p.Id == item.Id & p.Stockquantity > 0)
                        .CountAsync() > 0 ? await _context.Products
                        .Where(p => p.Id == item.Id)
                        .Select(p => new ProductListBestProduct
                        {
                            Sellerid = p.Sellerproducts
                                .Where(sp => sp.Stockquantity > 0 & sp.Productid == p.Id)
                                .OrderBy(sp => sp.Price)
                                .Select(sp => sp.Sellerid)
                                .FirstOrDefault(),
                            Price = p.Sellerproducts
                                .Where(sp => sp.Stockquantity > 0 & sp.Productid == p.Id)
                                .OrderBy(sp => sp.Price)
                                .Select(sp => sp.Price)
                                .FirstOrDefault(),
                            Stockquantity = p.Sellerproducts
                                .Where(sp => sp.Stockquantity > 0 & sp.Productid == p.Id)
                                .OrderBy(sp => sp.Price)
                                .Select(sp => sp.Stockquantity)
                                .FirstOrDefault()
                        })
                        .FirstOrDefaultAsync() : null
                };
                products.Add(dto);
            }
            
            return (products, totalPages);
        }
    }
}