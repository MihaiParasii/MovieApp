using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChineseNetflix.Models.Enums;

namespace ChineseNetflix.Models;

public class Movie(
    uint id,
    string title,
    List<Actor> actorsList,
    List<EMovieGenre> movieGenres,
    float rate,
    DateTime date)
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; init; } = id;

    [Required, MaxLength(30)] public string Title { get; init; } = title;
    public List<Actor> ActorsList { get; init; } = actorsList;
    public List<EMovieGenre> Genres { get; init; } = movieGenres;
    [Required] public float Rate { get; set; } = rate;
    [Required] public DateTime Date { get; set; } = date;

    private readonly List<float> _rates = [rate];

    public void RateMovie(float userRate)
    {
        _rates.Add(userRate);
        Rate = _rates.Sum() / _rates.Count;
    }
}
