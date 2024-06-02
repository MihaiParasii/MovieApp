using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChineseNetflix.Models;

public class Customer
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required int CustomerId { get; init; }

    [Required, MaxLength(40)] public required string Email { get; init; }

    [Required, MaxLength(64)] public required string Password { get; init; }

    // private GenresRates _genresRates = genresRates;
    private readonly List<Movie> _watchedMovies = [];
    private readonly List<Movie> _ratedMovies = [];
}
