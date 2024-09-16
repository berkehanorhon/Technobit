using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Interfaces;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(TechnobitpublicContext context)
            : base(context)
        {
        }
    }
}