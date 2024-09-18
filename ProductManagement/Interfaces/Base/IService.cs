using MediatR;

namespace ProductManagement.Interfaces;

public interface IService<C, R, U> 
    where C : class
    where R : class
    where U : class
{
    Task<IEnumerable<R>> GetAllAsync();
    Task<R?> GetByIdAsync(int id);
    Task<int> CreateAsync(C createDTO);
    Task<U> UpdateAsync(U updateDTO);
    Task<Unit> DeleteAsync(int id);
}
