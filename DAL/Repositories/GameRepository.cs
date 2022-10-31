using Games.DAL.EF;
using Games.DAL.Entities;
using Games.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Games.DAL.Repositories;

public class GameRepository : GenericRepository<Game>, IGameRepository
{
    public GameRepository(DataContext context) : base(context) { }

    public override async Task<Game?> GetAsync(Guid id)
    {
        return await _context.Games
            .Include(g => g.Genres)
            .FirstOrDefaultAsync(g => g.Id == id);
    }
    
    public override async Task<Game?> GetAsync(string name)
    {
        return await _context.Games
            .Include(g => g.Genres)
            .FirstOrDefaultAsync(i => i.Name == name);
    }

    public async Task<IEnumerable<Game>> GetAllGamesAsync()
    {
        return await _context.Games
            .Include(g=>g.Genres)
            .ToListAsync();
    }
}
