namespace ProductManagement.Interfaces;

public interface IService<C, R, U> 
    where C : class
    where R : class
    where U : class
{
    Task<IEnumerable<R>> GetAllAsync();
    Task<R?> GetByIdAsync(int id);
    Task<R> CreateAsync(C createDTO);
    Task<U> UpdateAsync(U updateDTO);
    Task DeleteAsync(int id);
}
