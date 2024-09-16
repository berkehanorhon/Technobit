using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Interfaces;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(TechnobitpublicContext context)
            : base(context)
        {
        }
    }
}
