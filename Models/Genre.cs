using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChineseNetflix.Models;

public class Genre
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [Required, MaxLength(30)] public required string Name { get; init; }
    
    public ICollection<Movie>? Movies { get; init; }
    
    public override string ToString() => Name;
}
