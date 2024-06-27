using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChineseNetflix.Models;

public class Rate
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    public AppUser User { get; set; } = null!;
    public MovieDetail MovieDetail { get; set; } = null!;
    public int Value { get; set; }
}
