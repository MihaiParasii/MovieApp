using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChineseNetflix.Models;

public class MovieComment
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [Required] public required MovieDetail MovieDetail { get; init; } = null!;
    [Required] public required AppUser User { get; init; } = null!;

    [Required, DataType(DataType.DateTime)]
    public required DateTime DateTime { get; init; }

    [Required] public int CountLikes { get; private set; } = 0;
    public  ICollection<CommentLike>? Likes { get; init; } = null!;
    [Required, MaxLength(500)] public required string Comment { get; init; } = null!;

    public void IncrementCountLikes()
    {
        ++CountLikes;
    }

    public void DecrementCountLikes()
    {
        --CountLikes;
    }
}
