using ChineseNetflix.Data;
using ChineseNetflix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ChineseNetflix.Controllers;

public class GenreController(NetflixContext context) : Controller
{
    public IActionResult AddGenre()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddGenreToDb(string genreName)
    {
        context.Genres.Add(new Genre { Name = genreName });
        context.SaveChanges();
        return View("AddGenre");
    }
}
