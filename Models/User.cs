using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChineseNetflix.Models;

public abstract class User(uint userId, string name, string surname, string email, string password, DateTime birthDate)
    : Person(name, surname, birthDate)
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint UserId { get; init; } = userId;

    [Required, MaxLength(40)] public string Email { get; set; } = email;
    [Required, MaxLength(64)] public string Password { get; set; } = password;
}
