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
        return View();
    }

    public IActionResult Privacy()
    {
        var movies = _context.Movies;

        return View(movies);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public IActionResult AddActor(string name, string surname)
    {
        Actor actor = new Actor
        {
            Name = name,
            Surname = surname,
            BirthDate = DateTime.Today
        };
        _context.Actors.Add(actor);

        _context.SaveChanges();

        return View("Index");
    }

    
}
