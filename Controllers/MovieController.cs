using ChineseNetflix.Data;
using ChineseNetflix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ChineseNetflix.Controllers;

public class MovieController(AppDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var movies = context.Movies.Include(m => m.MovieDetail);
        return View(await movies.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await context.Movies
            .Include(m => m.MovieDetail)
                .ThenInclude(md => md.Comments)
                    .ThenInclude(c => c.Likes)!
                        .ThenInclude(l => l.User)
            .Include(m => m.MovieDetail)
                .ThenInclude(md => md.Comments)
                    .ThenInclude(c => c.User)
            .Include(m => m.Actors)
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    public IActionResult Create()
    {
        ViewData["ActorsId"] = context.Actors.ToList();
        ViewData["GenresId"] = context.Genres.ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Date")] MovieDetail movieDetail,
        ICollection<int> actorsId, ICollection<int> genresId)
    {
        List<Actor> foundedActors = [];
        foundedActors.AddRange(from id in actorsId
            from contextActor in context.Actors
            where contextActor.Id == id
            select contextActor);

        List<Genre> foundedGenres = [];
        foundedGenres.AddRange(from id in genresId
            from contextGenre in context.Genres
            where contextGenre.Id == id
            select contextGenre);

        var newMovie = new Movie
        {
            Actors = foundedActors,
            Genres = foundedGenres,
            MovieDetail = movieDetail
        };

        context.Movies.Add(newMovie);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movieDetail = await context.MovieDetail.FindAsync(id);

        if (movieDetail == null)
        {
            return NotFound();
        }

        ViewData["MovieId"] = new SelectList(context.Movies, "Id", "Id", movieDetail.MovieId);
        return View(movieDetail);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("MovieId,Title,Date")] MovieDetail movieDetail)
    {
        if (id != movieDetail.MovieId)
        {
            return NotFound();
        }

        try
        {
            context.Update(movieDetail);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MovieDetailExists(movieDetail.MovieId))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movieDetail = await context.MovieDetail
            .Include(m => m.Movie)
            .FirstOrDefaultAsync(m => m.MovieId == id);

        if (movieDetail == null)
        {
            return NotFound();
        }

        return View(movieDetail);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var movie = await context.Movies.FindAsync(id);

        if (movie != null)
        {
            context.Movies.Remove(movie);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieDetailExists(int id)
    {
        return context.MovieDetail.Any(e => e.MovieId == id);
    }

    public async Task<IActionResult> RateMovie([Bind("MovieId,Title,Date")] MovieDetail movieDetail,
        string userId, int rateValue)
    {
        var movie = await context.Movies
            .Include(m => m.MovieDetail)
            .ThenInclude(md => md.Rates)
            .FirstOrDefaultAsync(m => m.Id == movieDetail.MovieId);

        var user = await context.AppUsers
            .Include(a => a.Rates)
            .FirstOrDefaultAsync(a => a.Id == userId);

        if (movie == null)
        {
            return NotFound();
        }

        Rate? rate = await context.Rates.SingleOrDefaultAsync(r =>
            r.MovieDetail.MovieId == movieDetail.MovieId && r.User.Id == userId);

        if (rate == null)
        {
            context.Rates.Add(CreateRate(movie.MovieDetail, user!, rateValue));
            context.Update(movie);
        }
        else
        {
            rate.Value = rateValue;
            context.Update(rate);
        }

        movie.MovieDetail.UpdateRate();

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Details), new { id = movieDetail.MovieId });
    }

    private static Rate CreateRate(MovieDetail movieDetail, AppUser user, int rate)
    {
        return new Rate
        {
            MovieDetail = movieDetail,
            User = user,
            Value = rate
        };
    }
}
