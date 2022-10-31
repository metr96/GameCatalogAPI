using Games.DAL.EF;
using Games.DAL.Entities;
using Games.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Games.DAL.Repositories;

public abstract class GenericRepository<T> : IGenericRepository<T>
    where T : EntityBase
{
    private protected DataContext _context;
    public GenericRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateAsync(T item)
    {
        await _context.Set<T>().AddAsync(item);
        await _context.SaveChangesAsync();
        return item.Id;
    }

    public async Task<IEnumerable<T>?> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public abstract Task<T?> GetAsync(Guid id);

    public abstract Task<T?> GetAsync(string name);

    public async Task<bool> UpdateAsync(T item)
    {
        _context.Set<T>().Update(item);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(T item)
    {
        _context.Set<T>().Remove(item);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> HasAsync(string name)
    {
        return await _context.Set<T>()
            .AnyAsync(i => i.Name == name);
    }

    public async Task<bool> HasAsync(Guid id)
    {
        return await _context.Set<T>()
            .AnyAsync(i => i.Id == id);
    }
}
