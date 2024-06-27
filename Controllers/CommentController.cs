using ChineseNetflix.Data;
using ChineseNetflix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace ChineseNetflix.Controllers;

public class CommentController(AppDbContext context) : Controller
{
    public async Task<IActionResult> Create(int movieId, string userId, DateTime dateTime, string comment)
    {
        var movie = await context.MovieDetail.FirstAsync(m => m.MovieId == movieId);
        var user = await context.AppUsers.FirstAsync(u => u.Id == userId);

        context.Comments.Add(CreateMovieComment(user, movie, dateTime, comment));
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Details), nameof(Movie), new { id = movieId });
    }

    public async Task<IActionResult> Delete(int id)
    {
        var comment = await context.Comments
            .Include(c => c.User)
            .Include(c => c.MovieDetail)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (comment == null)
        {
            return NotFound();
        }

        context.Comments.Remove(comment);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Details), nameof(Movie), new { id = comment.MovieDetail.MovieId });
    }

    public async Task<IActionResult> Edit(int id)
    {
        var comment = await context.Comments
            .Include(c => c.MovieDetail)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (comment == null)
        {
            return NotFound();
        }

        ViewData["commentValue"] = comment.Comment;

        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(int id,
        [Bind("Id,MovieDetail,User,DateTime,Comment")]
        MovieComment movieComment)
    {
        if (id != movieComment.Id)
        {
            return NotFound();
        }

        context.Update(movieComment);
        await context.SaveChangesAsync();

        var movieDetail = context.MovieDetail.Include(md => md.Comments.Where(c => c.Id == movieComment.Id)).First();


        return RedirectToAction(nameof(Details), nameof(Movie), new { id = movieDetail.MovieId });
    }

    public async Task<IActionResult> Like(int commentId, string userId)
    {
        var user = await context.AppUsers.FirstAsync(u => u.Id.Equals(userId));
        var comment = await context.Comments
            .Include(c => c.MovieDetail)
            .FirstAsync(c => c.Id == commentId);

        await context.CommentLikes.AddAsync(CreateCommentLike(comment, user));
        comment.IncrementCountLikes();
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Details), nameof(Movie), new { id = comment.MovieDetail.MovieId });
    }

    public async Task<IActionResult> UnLike(int commentId, string userId)
    {
        var commentLike = await context.CommentLikes
            .Include(cl => cl.Comment)
            .ThenInclude(c => c.User)
            .Include(cl1 => cl1.Comment)
            .ThenInclude(c => c.MovieDetail)
            .FirstAsync(cl => cl.Comment.Id == commentId && cl.User.Id.Equals(userId));

        commentLike.Comment.DecrementCountLikes();
        context.CommentLikes.Remove(commentLike);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Details), nameof(Movie), new { id = commentLike.Comment.MovieDetail.MovieId });
    }

    private static MovieComment CreateMovieComment(AppUser user, MovieDetail movieDetail, DateTime dateTime,
        string comment) => new()
    {
        User = user,
        MovieDetail = movieDetail,
        Comment = comment,
        DateTime = dateTime
    };

    private static CommentLike CreateCommentLike(MovieComment comment, AppUser user) => new()
    {
        Comment = comment,
        User = user
    };
}
