using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Interfaces;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class SellerRepository : BaseRepository<Seller>
    {
        public SellerRepository(TechnobitpublicContext context)
            : base(context)
        {
        }
    }
}