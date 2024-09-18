using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Interfaces;
using ProductManagement.Models;

namespace ProductManagement.Repositories;

public class BaseRepository<T> : IRepository<T> where T : class
{
    private readonly TechnobitpublicContext _context;
    private DbSet<T> Entities => _context.Set<T>();
    public BaseRepository(TechnobitpublicContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Entities.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await Entities.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await Entities.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        Entities.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Unit> DeleteAsync(T entity)
    {
        Entities.Remove(entity);
        await _context.SaveChangesAsync();
        return Unit.Value;
    }
}