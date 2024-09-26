using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Interfaces;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class ProductImageRepository : BaseRepository<Productimage>
    {
        public ProductImageRepository(TechnobitpublicContext context)
            : base(context)
        {
        }
    }
}