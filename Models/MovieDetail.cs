using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChineseNetflix.Models;

public class MovieDetail
{
    [Key, ForeignKey("Movie")] public int MovieId { get; init; }

    [Required, MaxLength(30)] public required string Title { get; init; }
    [Required] public DateOnly Date { get; init; }
    public Movie Movie { get; init; } = null!;
    public ICollection<Rate> Rates { get; set; } = null!;
    public ICollection<MovieComment> Comments { get; set; } = [];
    public float? Rate { get; private set; }

    public void UpdateRate()
    {
        Rate = (float)Rates.Average(r => r.Value);
    }
}
