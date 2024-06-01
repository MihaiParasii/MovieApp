using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChineseNetflix.Models;

public class Actor(string name, string surname, DateTime birthDate) : Person(name, surname, birthDate)
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ActorId { get; init; }

    public override string ToString() => $"{Name} {Surname}";
}
