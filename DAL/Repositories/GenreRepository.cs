using Games.DAL.EF;
using Games.DAL.Entities;
using Games.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Games.DAL.Repositories;

public class GenreRepository : GenericRepository<Genre>, IGenreRepository
{
    public GenreRepository(DataContext context) : base(context) { }

    public override async Task<Genre?> GetAsync(Guid id)
    {
        return await _context.Genres
            .Include(gn => gn.Games)
            .ThenInclude(ga => ga.Genres)
            .FirstOrDefaultAsync(gn => gn.Id == id);
    }
    
    public override async Task<Genre?> GetAsync(string name)
    {
        return await _context.Genres
            .Include(gn => gn.Games)
            .ThenInclude(ga => ga.Genres)
            .FirstOrDefaultAsync(i => i.Name == name);
    }
}
