using System.Diagnostics;
using ChineseNetflix.Database;
using Microsoft.AspNetCore.Mvc;
using ChineseNetflix.Models;
using ChineseNetflix.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace ChineseNetflix.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly NetflixContext _context;
    
    public HomeController(ILogger<HomeController> logger, NetflixContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
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

        list.Sort((x, y) => DateTime.Compare(y.Date, x.Date));

        return View(list);
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

    [HttpPost]
    public IActionResult AddActor(string name, string surname)
    {
        Actor actor = new Actor(name, surname, DateTime.Today);
        _context.Actors.Add(actor);
        
        _context.SaveChanges();
        
        return View("Index");
    }
}
