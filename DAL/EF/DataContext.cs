using Games.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Games.DAL.EF;

public class DataContext : DbContext
{
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;

    public DataContext(DbContextOptions<DataContext> options)
       : base(options) => Database.Migrate();
}
