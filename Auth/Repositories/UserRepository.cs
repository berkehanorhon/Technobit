using Microsoft.EntityFrameworkCore;
using TechnoBit.Data;
using TechnoBit.Interfaces;
using TechnoBit.Models;

namespace TechnoBit.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbContext _context;
    private readonly DbSet<User> _dbSet;
    
    public UserRepository(ApplicationDbContext  context)
    {
        _context = context;
        _dbSet = _context.Set<User>();
    }
    
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByTokenAsync(string token)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.RefreshToken == token);
    }
    
    public async Task<User> AddUserAsync(User user)
    {
        await _dbSet.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _dbSet.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async void DeleteUserAsync(User user)
    {
        _dbSet.Remove(user);
        await _context.SaveChangesAsync();
    }
}