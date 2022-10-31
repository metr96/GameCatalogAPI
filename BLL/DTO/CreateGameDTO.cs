using System.ComponentModel.DataAnnotations;

namespace Games.BLL.DTO;

public class CreateGameDTO
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Developer { get; set; } = null!;
    [Required]
    public ICollection<string> Genres { get; set; } = null!;
}
