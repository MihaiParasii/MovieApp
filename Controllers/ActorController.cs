using ChineseNetflix.Data;
using ChineseNetflix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChineseNetflix.Controllers;

public class ActorController(AppDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await context.Actors.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var actor = await context.Actors
            .Include(a => a.Movies)!
            .ThenInclude(m => m.MovieDetail)
            .FirstOrDefaultAsync(a => a.Id == id);


        if (actor == null)
        {
            return NotFound();
        }

        return View(actor);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Surname,BirthDate")] Actor actor)
    {
        if (ModelState.IsValid)
        {
            context.Add(actor);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(actor);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var actor = await context.Actors.FindAsync(id);

        if (actor == null)
        {
            return NotFound();
        }

        return View(actor);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,BirthDate")] Actor actor)
    {
        if (id != actor.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(actor);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(actor.Id))
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

        return View(actor);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var actor = await context.Actors
            .FirstOrDefaultAsync(m => m.Id == id);

        if (actor == null)
        {
            return NotFound();
        }

        return View(actor);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var actor = await context.Actors.FindAsync(id);

        if (actor != null)
        {
            context.Actors.Remove(actor);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ActorExists(int id)
    {
        return context.Actors.Any(e => e.Id == id);
    }
}
