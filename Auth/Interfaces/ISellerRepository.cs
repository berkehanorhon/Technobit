using TechnoBit.Models;

namespace TechnoBit.Interfaces;

public interface ISellerRepository : IBaseRepository<Seller>
{
    Task<User?> GetSellerByUsernameAsync(string username);
}