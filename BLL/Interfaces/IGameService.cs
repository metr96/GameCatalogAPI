using Games.BLL.DTO;

namespace Games.BLL.Interfaces;

public interface IGameService
{
    Task<GameDTO> CreateAsync(CreateGameDTO dto);
    Task<IEnumerable<GameDTO>> GetAllGamesAsync();
    Task<GameDTO> GetGameAsync(Guid id);
    Task<GameDTO> GetGameAsync(string name);
    Task<GenreDTO> GetGenreAsync(string name);
    Task<IEnumerable<GameDTO>> GetGamesByGenreAsync(string genreName);
    Task<IEnumerable<GameDTO>> GetGamesByGenreAsync(Guid genreId);
    Task UpdateGameAsync(GameDTO dto);
    Task UpdateGenreAsync(GenreDTO dto);
    Task DeleteGameAsync(Guid id);
    Task DeleteGenreAsync(Guid id);
}