using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Interfaces;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class SellerProductRepository : BaseRepository<Sellerproduct>
    {
        public SellerProductRepository(TechnobitpublicContext context)
            : base(context)
        {
        }
    }
}