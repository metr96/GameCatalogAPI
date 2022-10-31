using Games.DAL.Entities;

namespace Games.DAL.Interfaces;

public interface IGameRepository : IGenericRepository<Game>
{
    Task<IEnumerable<Game>> GetAllGamesAsync();
}