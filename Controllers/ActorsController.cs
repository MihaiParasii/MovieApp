using ChineseNetflix.Data;
using ChineseNetflix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChineseNetflix.Controllers;

public class ActorsController(NetflixContext context) : Controller
{

    [HttpGet]
    public IActionResult Index(int movieId = -1)
    {
        var list = context.Actors.Include(actor => actor.Movies).ToList();

        List<Actor> actors = [];

        if (movieId != -1)
        {
            actors.AddRange(list.Where(actor =>
                actor.Movies.Contains(context.Movies.FirstOrDefault(movie => movie.Id == movieId))));
        }
        else
        {
            actors = list;
        }

        return View(actors);
    }

    [HttpGet]
    public IActionResult DetailedView(int id)
    {
        var list = context.Actors;

        foreach (Actor actor in list.Where(actor => actor.Id == id))
        {
            return View(actor);
        }

        return View("MovieNotFound");
    }

    [HttpPost]
    public IActionResult AddActorToDb(string name, string surname, DateOnly birthDate)
    {
        Actor newActor = new Actor
        {
            Name = name,
            Surname = surname,
            BirthDate = birthDate,
        };

        context.Actors.Add(newActor);
        context.SaveChanges();
        return View("AddActor", context.Actors);
    }

    public IActionResult AddActor()
    {
        return View("AddActor", context.Actors);
    }

    public IActionResult ShowMoviesWithActor(int targetId)
    {
        return View("Index", context.Actors.Where(actor => actor.Id == targetId));
    }
}
