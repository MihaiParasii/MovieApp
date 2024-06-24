using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ChineseNetflix.Models;

public class AppUser : IdentityUser
{
    [Required, MaxLength(40)] public required string Surname { get; set; }
    [Required, MaxLength(40)] public required string Nickname { get; set; }
}
