using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Interfaces;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class ProductDetailTagRepository : BaseRepository<Productdetailtag>
    {
        public ProductDetailTagRepository(TechnobitpublicContext context)
            : base(context)
        {
        }
    }
}
