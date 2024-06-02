using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChineseNetflix.Models.Enums;

namespace ChineseNetflix.Models;

public class Movie
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; init; }

    [Required, MaxLength(30)] public required string Title { get; init; }
    // public required List<Actor> ActorsList { get; init; }
    // public required List<EMovieGenre> Genres { get; init; }
    [Required] public float Rate { get; init; }
    [Required] public DateOnly Date { get; init; }

    // public List<float> Rates { get; init; } = null!;
}
