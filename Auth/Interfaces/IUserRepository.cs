using TechnoBit.Models;

namespace TechnoBit.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByTokenAsync(string token);

}