namespace Games.DAL.Entities;

public class Genre : EntityBase
{
    public ICollection<Game> Games { get; set; } = null!;
}
