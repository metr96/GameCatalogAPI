using System.ComponentModel.DataAnnotations;

namespace Games.BLL.DTO;

public class GenreDTO
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
}
