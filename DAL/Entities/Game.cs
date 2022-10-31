namespace Games.DAL.Entities;

public class Game : EntityBase
{ 
    public string Developer { get; set; } = null!;
    public ICollection<Genre> Genres { get; set; } = null!;
}
