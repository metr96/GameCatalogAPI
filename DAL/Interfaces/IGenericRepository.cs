using Games.DAL.Entities;

namespace Games.DAL.Interfaces;

public interface IGenericRepository<T> where T : EntityBase
{
    Task<Guid> CreateAsync(T item);
    Task<T?> GetAsync(Guid id);
    Task<T?> GetAsync(string name);
    Task<bool> UpdateAsync(T item);
    Task<bool> DeleteAsync(T item);
    Task<bool> HasAsync(string name);
    Task<bool> HasAsync(Guid id);
}