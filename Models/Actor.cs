using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChineseNetflix.Models;

public class Actor
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ActorId { get; init; }
    [Required, MaxLength(30)]public required string Name { get; init; }
    [Required, MaxLength(30)]public required string Surname { get; init; }
    public required DateTime BirthDate { get; init; }
    

    public override string ToString() => $"{Name} {Surname}";
}
