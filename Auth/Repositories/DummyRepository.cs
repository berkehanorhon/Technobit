using TechnoBit.Data;
using TechnoBit.Models;
using TechnoBit.Interfaces;
namespace TechnoBit.Repositories;

public class DummyRepository : BaseRepository<Dummy>, IDummyRepository
{
    public DummyRepository(ApplicationDbContext context) : base(context){}
    
}