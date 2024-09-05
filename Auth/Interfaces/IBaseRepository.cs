using TechnoBit.Data;

namespace TechnoBit.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<T> AddModelAsync(T model);

    Task<T> UpdateModelAsync(T model);

    void DeleteModelAsync(T model);
}