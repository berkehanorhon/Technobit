using Microsoft.EntityFrameworkCore;
using TechnoBit.Data;
using TechnoBit.Interfaces;
using TechnoBit.Models;

namespace TechnoBit.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;
    
    public BaseRepository(ApplicationDbContext  context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    
    public async Task<T> AddModelAsync(T model)
    {
        await _dbSet.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }
    
    public async Task<T> UpdateModelAsync(T model)
    {
        _dbSet.Update(model);
        await _context.SaveChangesAsync();
        return model;
    }
    
    public async void DeleteModelAsync(T model)
    {
        _dbSet.Remove(model);
        await _context.SaveChangesAsync();
    }
}