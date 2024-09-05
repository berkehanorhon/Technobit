using Microsoft.EntityFrameworkCore;
using TechnoBit.Data;
using TechnoBit.Interfaces;
using TechnoBit.Models;

namespace TechnoBit.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{

    public UserRepository(ApplicationDbContext context) : base(context){}
    
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
    
}