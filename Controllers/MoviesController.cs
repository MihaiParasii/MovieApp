using ChineseNetflix.Database;
using ChineseNetflix.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChineseNetflix.Controllers;

public class MoviesController : Controller
{
    private readonly NetflixContext _context;

    public MoviesController(NetflixContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult DetailedView(int id)
    {
        return View("MovieNotFound");
    }

    public IActionResult AddMoviePage()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddMovie(string title, double rate, DateOnly dateTime)
    {
        Movie movie = new Movie()
        {
            Title = title,
            Rate = Convert.ToSingle(rate),
            Date = dateTime
        };
        _context.Movies.Add(movie);

        _context.SaveChanges();
        return View("Page1", movie);
    }
}
