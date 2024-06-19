using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChineseNetflix.Models;

public class Customer
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required int Id { get; init; }

    [Required, MaxLength(40)] public required string Email { get; init; }

    [Required, MaxLength(64)] public required string Password { get; init; }

    private readonly List<MovieDetail> _watchedMovies = [];
    private readonly List<MovieDetail> _ratedMovies = [];
}
