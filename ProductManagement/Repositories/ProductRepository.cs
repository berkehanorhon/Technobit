using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.DTOs.Base;
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
                        .CountAsync() > 0
                        ? await _context.Products
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
                            .FirstOrDefaultAsync()
                        : null
                };
                products.Add(dto);
            }

            return (products, totalPages);
        }

        public async Task<ProductPageSendDTO?> GetProductPageSendDTOAsync(int productId)
        {
            List<BaseCategoryDTO> category_list = new List<BaseCategoryDTO>();
            int? categoryId = await _context.Products
                .Where(p => p.Id == productId)
                .Select(p => p.Categoryid)
                .FirstOrDefaultAsync();
            
            while (categoryId != null)
            {
                var new_category = await _context.Categories
                    .Where(c => c.Id == categoryId)
                    .Select(c => new BaseCategoryDTO
                    {
                        Name = c.Name,
                        Subcategoryid = c.Subcategoryid,
                        Description = c.Description
                    })
                    .FirstOrDefaultAsync();
                category_list.Add(new_category);
                categoryId = new_category?.Subcategoryid;
                
                
            }
            category_list.Sort((x, y) =>
            {
                // Subcategory ID kontrolü
                var xSubcategoryId = x?.Subcategoryid;
                var ySubcategoryId = y?.Subcategoryid;

                // Eğer biri null ise, onu en başa al
                if (xSubcategoryId == null && ySubcategoryId == null) return 0; // her ikisi de null ise eşit
                if (xSubcategoryId == null) return -1; // x null ise y'nin önünde olmalı
                if (ySubcategoryId == null) return 1;  // y null ise x'in önünde olmalı

                // Her iki subcategory ID mevcut ise karşılaştırma yap
                return xSubcategoryId.Value.CompareTo(ySubcategoryId.Value);
            });
            Console.WriteLine(category_list.ToString());
            var product = await _context.Products
                .Where(p => p.Id == productId)
                .Select(p => new ProductPageSendDTO
                {
                    name = p.Name,
                    description = p.Description,
                    price = _context.Sellerproducts
                        .Where(sp => sp.Productid == p.Id)
                        .Select(sp => sp.Price)
                        .FirstOrDefault(),
                    quantity = _context.Sellerproducts
                        .Where(sp => sp.Productid == p.Id)
                        .Select(sp => sp.Stockquantity)
                        .FirstOrDefault(),
                    sellerName = _context.Sellerproducts
                        .Where(sp => sp.Productid == p.Id)
                        .Select(sp => sp.Seller.Name)
                        .FirstOrDefault(),
                    sellerID = _context.Sellerproducts
                        .Where(sp => sp.Productid == p.Id)
                        .Select(sp => sp.Sellerid)
                        .FirstOrDefault(),
                    categories = category_list,
                    images = _context.Productimages
                        .Where(pi => pi.Productid == productId)
                        .Select(pi => pi.Imagepath)
                        .ToList(),
                    tags = _context.Productdetailtags
                        .Where(pt => pt.Productid == p.Id)
                        .OrderBy(p => p.Id)
                        .Select(pt => new BaseProductDetailTagDTO
                        {
                            Productid = pt.Productid,
                            Title = pt.Title,
                            Description = pt.Description
                        }
                        ).ToList()
                }).FirstOrDefaultAsync();

            return product;
        }
    }
}