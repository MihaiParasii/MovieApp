using ChineseNetflix.Data;
using ChineseNetflix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChineseNetflix.Controllers;

public class GenreController(AppDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await context.Genres.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genre = await context.Genres
            .Include(m => m.Movies)!
            .ThenInclude(m => m.MovieDetail)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (genre == null)
        {
            return NotFound();
        }

        return View(genre);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Genre genre)
    {
        if (ModelState.IsValid)
        {
            context.Add(genre);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(genre);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genre = await context.Genres.FindAsync(id);
        if (genre == null)
        {
            return NotFound();
        }
        return View(genre);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Genre genre)
    {
        if (id != genre.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(genre);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(genre.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(genre);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genre = await context.Genres
            .FirstOrDefaultAsync(m => m.Id == id);
        if (genre == null)
        {
            return NotFound();
        }

        return View(genre);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var genre = await context.Genres.FindAsync(id);
        if (genre != null)
        {
            context.Genres.Remove(genre);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GenreExists(int id)
    {
        return context.Genres.Any(e => e.Id == id);
    }
}