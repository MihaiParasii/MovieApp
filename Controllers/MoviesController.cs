using ChineseNetflix.Data;
using ChineseNetflix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NuGet.Packaging;

namespace ChineseNetflix.Controllers;

public class MoviesController(NetflixContext context) : Controller
{
    [HttpGet]
    public IActionResult Index(string title = "", int actorId = -1)
    {
        var list = context.Movies.Include(movie => movie.Actors)
            .Include(movie => movie.MovieDetail).ToList()
            .Where(movie => movie.MovieDetail.Title.Contains(title, StringComparison.CurrentCultureIgnoreCase))
            .ToList();
        List<Movie> foundedMovies = [];

        if (actorId > 0)
        {
            var targetActor = context.Actors.FirstOrDefault(actor => actor.Id == actorId);

            foundedMovies.AddRange(list
                .SelectMany(movie => movie.Actors, (movie, movieActor) => new { movie, movieActor })
                .Where(@t => @t.movieActor.Equals(targetActor))
                .Select(@t => @t.movie));
        }
        else
        {
            foundedMovies = list;
        }

        ViewData["Genres"] = context.Genres.ToList();
        return foundedMovies.Count != 0 ? View(foundedMovies) : View("MovieNotFound");
    }

    [HttpGet]
    public IActionResult DetailedView(int id)
    {
        var list = context.Movies.Include(movie => movie.Actors).Include(movie => movie.Genres)
            .Include(movie => movie.MovieDetail).ToList();

        foreach (Movie movie in list.Where(movie => movie.MovieDetail.MovieId == id))
        {
            return View(movie);
        }

        return View("MovieNotFound");
    }

    public IActionResult AddMovie()
    {
        ViewData["genres"] = context.Genres.ToList();
        return View(context.Actors);
    }


    [HttpPost]
    public IActionResult AddMovieToDb(string title, DateOnly releaseDate, ICollection<int> actorsId,
        ICollection<int> genresId)
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
            MovieDetail = new MovieDetail
            {
                Title = title,
                Date = releaseDate
            }
        };

        context.Movies.Add(newMovie);
        context.SaveChanges();

        ViewData["genres"] = context.Genres.ToList();
        return View("AddMovie", context.Actors);
    }

    // private static List<T> FindInContextGeneric<T>(DbSet<T> context, IEnumerable<int> idS) where T : class
    // {
    //     List<Actor> list = [];
    //     list.AddRange(idS
    //         .SelectMany(actorId => context, (entityId, entity) => new { entityId = entityId, entity = entity })
    //         .Where(@t => @t.entity. == @t.entityId)
    //         .Select(@t => @t.entity));
    //
    //     return list;
    // }
}
