using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ChineseNetflix.Models;

public class AppUser : IdentityUser
{
    [Required, MaxLength(40)] public required string Surname { get; set; }
    [Required, MaxLength(40)] public required string Nickname { get; set; }
    
    public List<Rate> Rates { get; init; } = [];
    public List<MovieComment> Comments { get; init; } = [];
    public List<CommentLike>? Likes { get; init; } = [];
    
}
