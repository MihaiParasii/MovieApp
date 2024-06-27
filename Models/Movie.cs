using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChineseNetflix.Models;

public class Movie
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [Required] public MovieDetail MovieDetail { get; init; } = null!;
    [Required] public ICollection<Actor> Actors { get; init; } = null!;
    [Required] public ICollection<Genre> Genres { get; init; } = null!;
}
