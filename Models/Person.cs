using System.ComponentModel.DataAnnotations;

namespace ChineseNetflix.Models;

public abstract class Person(string name, string surname, DateTime birthDate)
{
    [Required, MaxLength(30)] public string Name { get; init; } = name;
    [Required, MaxLength(30)] public string Surname { get; init; } = surname;
    public DateTime BirthDate { get; set; } = birthDate;
}
