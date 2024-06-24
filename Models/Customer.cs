using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChineseNetflix.Models;

public class Customer
{
    public required int Id { get; init; }
    
    private readonly List<MovieDetail> _watchedMovies = [];
    private readonly List<MovieDetail> _ratedMovies = [];
}
