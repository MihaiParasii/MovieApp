using System.Diagnostics;
using ChineseNetflix.Data;
using Microsoft.AspNetCore.Mvc;
using ChineseNetflix.Models;
using Microsoft.EntityFrameworkCore;

namespace ChineseNetflix.Controllers;

public class HomeController(ILogger<HomeController> logger, AppDbContext context) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index()
    {
        var list = context.Movies.Include(movie => movie.MovieDetail).ToList();

        list.Sort((x, y) => y.MovieDetail.Date.CompareTo(x.MovieDetail.Date));
        var movies = list.Take(3);
        return View(movies);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public ActionResult Search(string query)
    {
        var movies = context.Movies
            .Include(movie => movie.MovieDetail)
            .Where(movie => movie.MovieDetail.Title.Contains(query)).ToList();

        var actors = context.Actors
            .Where(actor => actor.Name.Contains(query) || actor.Surname.Contains(query)).ToList();
        
        var genres = context.Genres
            .Where(genre => genre.Name.Contains(query)).ToList();

        ViewData["movies"] = movies;
        ViewData["actors"] = actors;
        ViewData["genres"] = genres;
        return View();
    }
}
