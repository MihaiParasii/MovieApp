using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ChineseNetflix.Models;

public class CommentLike
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [Required] public required AppUser User { get; init; }
    [Required] public required MovieComment Comment { get; init; }
}
