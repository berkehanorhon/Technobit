using Microsoft.EntityFrameworkCore;
using TechnoBit.Data;
using TechnoBit.Interfaces;
using TechnoBit.Models;

namespace TechnoBit.Repositories;

public class SellerRepository
{
    
    private readonly DbContext _context;
    protected readonly DbSet<Seller> _dbSet;
    
    public SellerRepository(ApplicationDbContext  context)
    {
        _context = context;
        _dbSet = _context.Set<Seller>();
    }
    
    public async Task<Seller?> GetSellerByIdAsync(int Id)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Id == Id);
    }
    
}