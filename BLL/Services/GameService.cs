using AutoMapper;
using Games.BLL.DTO;
using Games.BLL.Infrastructure;
using Games.BLL.Interfaces;
using Games.DAL.Entities;
using Games.DAL.Interfaces;

namespace Games.BLL.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepo;
    private readonly IGenreRepository _genreRepo;
    private readonly IMapper _gameMapper;

    public GameService(IGameRepository gameRepo,
        IGenreRepository genreRepo, IMapper gameMapper)
    {
        _gameRepo = gameRepo;
        _genreRepo = genreRepo;
        _gameMapper = gameMapper;
    }

    public async Task<GameDTO> CreateAsync(CreateGameDTO dto)
    {
        if (await _gameRepo.HasAsync(dto.Name))
            throw new DataConflictException(
                $"Game with name '{dto.Name}' already exists");

        var genres = await FindOrCreateGenres(dto.Genres);

        var game = _gameMapper.Map<Game>(dto);
        game.Genres = genres;

        await _gameRepo.CreateAsync(game);

        return _gameMapper.Map<GameDTO>(game);
    }

    public async Task<IEnumerable<GameDTO>> GetAllGamesAsync()
    {
        var games = await _gameRepo.GetAllGamesAsync();

        if (!games.Any())
            throw new NotFoundException("Can't find any game in DB");

        return _gameMapper.Map<IEnumerable<GameDTO>>(games);
    }

    public async Task<GameDTO> GetGameAsync(Guid id)
    {
        var game = await _gameRepo.GetAsync(id);
        if (game == null)
            throw new NotFoundException(
                $"Can't find game with id: {id}");

        return _gameMapper.Map<GameDTO>(game);
    }

    public async Task<GameDTO> GetGameAsync(string name)
    {
        var game = await _gameRepo.GetAsync(name);
        if (game == null)
            throw new NotFoundException(
                $"Can't find game with name: {name}");

        return _gameMapper.Map<GameDTO>(game);
    }

    public async Task<GenreDTO> GetGenreAsync(string name)
    {
        var genre = await _genreRepo.GetAsync(name);
        if (genre == null)
            throw new NotFoundException(
                $"Can't find genre with name: {name}");

        return _gameMapper.Map<GenreDTO>(genre);
    }

    public async Task<IEnumerable<GameDTO>> GetGamesByGenreAsync(string genreName)
    {
        var genre = await _genreRepo.GetAsync(genreName);
        if (genre == null)
            throw new NotFoundException(
                $"Can't find genre with name: {genreName}");

        return _gameMapper.Map<IEnumerable<GameDTO>>(genre.Games);
    }

    public async Task<IEnumerable<GameDTO>> GetGamesByGenreAsync(Guid genreId)
    {
        var genre = await _genreRepo.GetAsync(genreId);
        if (genre == null)
            throw new NotFoundException(
                $"Can't find genre with id: {genreId}");

        return _gameMapper.Map<IEnumerable<GameDTO>>(genre.Games);
    }

    public async Task UpdateGameAsync(GameDTO dto)
    {
        var game = await _gameRepo.GetAsync(dto.Id);
        if (game == null)
            throw new NotFoundException(
                $"Can't update game with id: '{dto.Id}', cause it doesn't exist");

        var genres = await FindOrCreateGenres(dto.Genres);

        _gameMapper.Map<GameDTO, Game>(dto, game);
        game.Genres = genres;

        await _gameRepo.UpdateAsync(game);
    }

    public async Task UpdateGenreAsync(GenreDTO dto)
    {
        var genre = await _genreRepo.GetAsync(dto.Id);
        if (genre == null)
            throw new NotFoundException(
                $"Can't update genre with id: '{dto.Id}', cause it doesn't exist");

        _gameMapper.Map<GenreDTO, Genre>(dto, genre);

        await _genreRepo.UpdateAsync(genre);
    }

    public async Task DeleteGameAsync(Guid id)
    {
        await DeleteItemAsync<Game>(_gameRepo, id);
    }

    public async Task DeleteGenreAsync(Guid id)
    {
        await DeleteItemAsync<Genre>(_genreRepo, id);
    }

    private async Task DeleteItemAsync<T>(IGenericRepository<T> repo, Guid id)
        where T : EntityBase
    {
        var item = await repo.GetAsync(id);
        if (item == null)
            throw new NotFoundException(
                $"Can't find {typeof(T).Name} with id: {id}");

        if (!await repo.DeleteAsync(item))
            throw new DataConflictException(
                $"Can't delete {typeof(T).Name} with id: {id}");
    }

    private async Task<List<Genre>> FindOrCreateGenres(
        ICollection<string> genreNames)
    {
        List<Genre> genres = new();

        foreach (var genreName in genreNames)
        {
            var genre = await _genreRepo.GetAsync(genreName);
            if (genre == null)
            {
                genre = new Genre() { Name = genreName };
                await _genreRepo.CreateAsync(genre);
            }
            genres.Add(genre);
        }

        return genres;
    }
}
