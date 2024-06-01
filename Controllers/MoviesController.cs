using ChineseNetflix.Models;
using ChineseNetflix.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ChineseNetflix.Controllers;

public class MoviesController : Controller
{
    [HttpGet]
    public IActionResult Index(string title = "", string sortType = "")
    {
        Actor johnyDepp = new Actor("Johny", "Depp", DateTime.Today);
        Actor jasonStatham = new Actor("Jason", "Statham", DateTime.Today);
        Movie movie1 = new Movie(1, "Movie1", [johnyDepp], [EMovieGenre.Drama], 5.0f, new DateTime(1994, 02, 15));
        Movie movie2 = new Movie(2, "Movie2", [johnyDepp], [EMovieGenre.Action], 5.0f, new DateTime(2015, 12, 1));
        Movie movie3 = new Movie(3, "Movie3", [johnyDepp], [EMovieGenre.Action], 5.0f, new DateTime(2011, 5, 30));
        Movie movie4 = new Movie(4, "Movie4", [johnyDepp, jasonStatham], [EMovieGenre.Action], 5.0f,
            new DateTime(2023, 10, 13));
        Movie movie5 = new Movie(5, "Mova5", [johnyDepp], [EMovieGenre.Action, EMovieGenre.Comedy], 5.0f,
            new DateTime(2008, 2, 19));
        Movie movie6 = new Movie(6, "Action2", [johnyDepp], [EMovieGenre.Action], 5.0f, new DateTime(1999, 8, 7));
        List<Movie> list = [movie1, movie2, movie3, movie4, movie5, movie6];
        List<Movie> foundedMovies = [];

        foundedMovies.AddRange(list.Where(movie =>
            movie.Title.Contains(title, StringComparison.CurrentCultureIgnoreCase)));

        switch (sortType)
        {
            case "releaseDate":
                list.Sort((x, y) => DateTime.Compare(y.Date, x.Date));
                break;
            case "title":
                list.Sort((x, y) => string.CompareOrdinal(y.Title, x.Title));
                break;
            case "rate":
                list.Sort((x, y) => x.Rate.CompareTo(y.Rate));
                break;
        }


        return foundedMovies.Count != 0 ? View(foundedMovies) : View("MovieNotFound");
    }

    [HttpGet]
    public IActionResult DetailedView(int id)
    {
        Actor johnyDepp = new Actor("Johny", "Depp", DateTime.Today);
        Actor jasonStatham = new Actor("Jason", "Statham", DateTime.Today);

        Movie movie1 = new Movie(1, "Movie1", [johnyDepp], [EMovieGenre.Drama], 5.0f, new DateTime(1994, 02, 15));
        Movie movie2 = new Movie(2, "Movie2", [johnyDepp], [EMovieGenre.Action], 5.0f, new DateTime(2015, 12, 1));
        Movie movie3 = new Movie(3, "Movie3", [johnyDepp], [EMovieGenre.Action], 5.0f, new DateTime(2011, 5, 30));
        Movie movie4 = new Movie(4, "Movie4", [johnyDepp, jasonStatham], [EMovieGenre.Action], 5.0f,
            new DateTime(2023, 10, 13));
        Movie movie5 = new Movie(5, "Mova5", [johnyDepp], [EMovieGenre.Action, EMovieGenre.Comedy], 5.0f,
            new DateTime(2008, 2, 19));
        Movie movie6 = new Movie(6, "Action2", [johnyDepp], [EMovieGenre.Action], 5.0f, new DateTime(1999, 8, 7));
        List<Movie> list = [movie1, movie2, movie3, movie4, movie5, movie6];

        foreach (Movie movie in list.Where(movie => movie.Id == id))
        {
            return View(movie);
        }

        return View("MovieNotFound");
    }
}
