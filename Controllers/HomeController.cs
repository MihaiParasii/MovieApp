using System.Diagnostics;
using ChineseNetflix.Data;
using Microsoft.AspNetCore.Mvc;
using ChineseNetflix.Models;
using Microsoft.EntityFrameworkCore;

namespace ChineseNetflix.Controllers;

public class HomeController(ILogger<HomeController> logger, NetflixContext context) : Controller
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
}
