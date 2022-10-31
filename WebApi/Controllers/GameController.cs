using Games.BLL.DTO;
using Games.BLL.Interfaces;
using Games.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesWebApi.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]/[action]")]
public class GameController : ControllerBase
{
    private IGameService _gameService;
    
    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }
        

    /// <summary>
    /// Creates a game
    /// </summary>
    /// <param name="game">CreateGameDTO</param>
    /// <returns>CreatedAtActionResult</returns>
    /// <response code="201">Success</response>
    /// <response code="409">If game with this name already exists</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<GameDTO>> PostAsync(CreateGameDTO game)
    {
        var dto = await _gameService.CreateAsync(game);
        // Can't use nameof(GetGameAsync) due to ASP.NET Async naming bug
        return CreatedAtAction("GetGame", new { id = dto.Id }, dto);
    }

    /// <summary>
    /// Gets a list of games
    /// </summary>
    /// <returns>IEnumerable of all GameDTOs</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If no games exist</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GameDTO>>> GetAllGamesAsync()
    {
        var result = await _gameService.GetAllGamesAsync();
        return Ok(result);
    }

    /// <summary>
    /// Gets a game by its ID 
    /// </summary>
    /// <param name="id">Game ID (Guid)</param>
    /// <returns>GameDTO</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If no game with this ID was found</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GameDTO>> GetGameAsync(
        Guid id)
    {
        var result = await _gameService.GetGameAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// Gets a game by its name 
    /// </summary>
    /// <param name="name">Game name</param>
    /// <returns>GameDTO</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If no game with this name was found</response>
    [HttpGet("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GameDTO>> GetGameAsync(
        string name)
    {
        var result = await _gameService.GetGameAsync(name);
        return Ok(result);
    }

    /// <summary>
    /// Gets a genre by its name
    /// </summary>
    /// <param name="name">Genre name</param>
    /// <returns>GenreDTO</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If no genre with this name was found</response>
    [HttpGet("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GenreDTO>> GetGenreAsync(string name)
    {
        var result = await _gameService.GetGenreAsync(name);
        return Ok(result);
    }

    /// <summary>
    /// Gets all games of this genre by its ID
    /// </summary>
    /// <param name="genreId">Genre ID</param>
    /// <returns>IEnumerable of GameDTO</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If no genre with this name was found</response>
    [HttpGet("{genreId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GameDTO>>> GetGamesByGenreAsync(
        Guid genreId)
    {
        var result = await _gameService.GetGamesByGenreAsync(genreId);
        return Ok(result);
    }

    /// <summary>
    /// Gets all games of this genre by its name
    /// </summary>
    /// <param name="genre">Genre name</param>
    /// <returns>IEnumerable of GameDTO</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If no genre with this ID was found</response>
    [HttpGet("{genre}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GameDTO>>> GetGamesByGenreAsync(
        string genre)
    {
        var result = await _gameService.GetGamesByGenreAsync(genre);
        return Ok(result);
    }

    /// <summary>
    /// Updates game
    /// </summary>
    /// <param name="game">Game name</param>
    /// <returns>GameDTO from put request body</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If no game with ID from body was found</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GameDTO>> UpdateGameAsync(GameDTO game)
    {
        await _gameService.UpdateGameAsync(game);
        return Ok(game);
    }

    /// <summary>
    /// Updates genre
    /// </summary>
    /// <param name="genre">Genre name</param>
    /// <returns>GenreDTO from put request body</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If no genre with ID from body was found</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GenreDTO>> UpdateGenreAsync(GenreDTO genre)
    {
        await _gameService.UpdateGenreAsync(genre);
        return Ok(genre);
    }

    /// <summary>
    /// Deletes game
    /// </summary>
    /// <param name="id">Game ID</param>
    /// <returns>ID of deleted game</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If no game with this ID was found</response>
    /// <response code="409">If can't delete game</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DeleteGameAsync(Guid id)
    {
        await _gameService.DeleteGameAsync(id);
        return Ok();
    }

    /// <summary>
    /// Deletes genre
    /// </summary>
    /// <param name="id">Genre ID</param>
    /// <returns>ID of deleted genre</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If no genre with this ID was found</response>
    /// <response code="409">If can't delete genre</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DeleteGenreAsync(Guid id)
    {
        await _gameService.DeleteGenreAsync(id);
        return Ok();
    }
}
