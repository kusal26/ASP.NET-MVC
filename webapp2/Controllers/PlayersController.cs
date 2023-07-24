using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using webapp2.Data;
using webapp2.Models;

namespace webapp2.Controllers
{
    public class PlayersController : Controller
    {
        private readonly webapp2Context _context;

        public PlayersController(webapp2Context context)
        {
            _context = context;
        }

        // GET: Players
        public async Task<IActionResult> Index(string sortOrder)
        {
            


            
        /*return View(await webapp2Context.ToListAsync())*/
        

        ViewData["PlayerNameSort"] = string.IsNullOrEmpty(sortOrder) ? "playername_desc" : "";
        ViewData["PositionSort"] = sortOrder == "Position" ? "position_desc" : "Position";
        ViewData["DatejoinedSort"] = sortOrder == "Datejoined" ? "date_desc" : "Datejoined";
        ViewData["ClubSort"] = sortOrder == "Club" ? "Club_desc" : "Club";
        ViewData["Countrysort"] = sortOrder == "Country" ? "Country_desc" : "Country";
            var webapp2Context = _context.Player.Include(p => p.Club).Include(p => p.Country);
            switch (sortOrder)
            {
                case "playername_desc":
                    webapp2Context = webapp2Context.OrderByDescending(p => p.PlayerName).Include(p => p.Club).Include(p => p.Country);
                    break;
                case "Position":
                    webapp2Context = webapp2Context.OrderBy(p => p.Position).Include(p => p.Club).Include(p => p.Country);
                    break;
                case "position_desc":
                    webapp2Context = webapp2Context.OrderByDescending(p => p.Position).Include(p => p.Club).Include(p => p.Country);
                    break;
                case "Datejoined":
                    webapp2Context = webapp2Context.OrderBy(p => p.Datejoined).Include(p => p.Club).Include(p => p.Country);
                    break;
                case "date_desc":
                    webapp2Context = webapp2Context.OrderByDescending(p => p.Datejoined).Include(p => p.Club).Include(p => p.Country);
                    break;
                case "Club":
                    webapp2Context= webapp2Context.OrderBy(p => p.Club).Include(p => p.Club).Include(p => p.Country);
                    break;
                case "Club_desc":
                    webapp2Context = webapp2Context.OrderByDescending(p => p.Club).Include(p => p.Club).Include(p => p.Country);
                    break;
                case "Country":
                    webapp2Context = webapp2Context.OrderBy(p => p.Country).Include(p => p.Club).Include(p => p.Country);
                    break;
                case "Country_desc":
                    webapp2Context = webapp2Context.OrderByDescending(p => p.Country).Include(p => p.Club).Include(p => p.Country);
                    break;
                default:
                    webapp2Context = webapp2Context.OrderBy(p => p.PlayerName).Include(p => p.Club).Include(p => p.Country);
                    break;
            }

            return View(await webapp2Context.ToListAsync());

        }





        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Player == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .Include(p => p.Club)
                .Include(p => p.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            var clubs = _context.Club.OrderBy(c => c.ClubName).ToList();
            var countries = _context.Country.OrderBy(c => c.CountryName).ToList();          
            ViewData["ClubId"] = new SelectList(clubs, "Id", "ClubName");
            ViewData["CountryId"] = new SelectList(countries, "Id", "CountryName");
            return View();
        }

        // POST: Players/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlayerName,Position,Datejoined,ClubId,CountryId")] Player player)
        {
            //    if (!ModelState.IsValid)
            //    {
            //        foreach (var key in ModelState.Keys)
            //        {
            //            var errors = ModelState[key].Errors;
            //            foreach (var error in errors)
            //            {
            //                var errorMessage = error.ErrorMessage;
            //                Console.WriteLine("Error msg="+errorMessage);
            //                // Log or display the error message as needed
            //            }
            //        }

            // Handle the invalid data or return the view with error messages
            // For example:
            // return View(player);
            // }
            //   ModelState.IsValid == false;


            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            }



            ViewData["ClubId"] = new SelectList(_context.Set<Club>(), "Id", "ClubName", player.ClubId);
            ViewData["CountryId"] = new SelectList(_context.Set<Country>(), "Id", "CountryName", player.CountryId);
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Player == null)
            {
                return NotFound();
            }

            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            ViewData["ClubId"] = new SelectList(_context.Set<Club>(), "Id", "ClubName", player.ClubId);
            ViewData["CountryId"] = new SelectList(_context.Set<Country>(), "Id", "CountryName", player.CountryId);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlayerName,Position,Datejoined,ClubId,CountryId")] Player player)
        {
            if (id != player.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.Id))
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


            ViewData["ClubId"] = new SelectList(_context.Set<Club>(), "Id", "ClubName", player.ClubId);
            ViewData["CountryId"] = new SelectList(_context.Set<Country>(), "Id", "CountryName", player.CountryId);
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Player == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .Include(p => p.Club)
                .Include(p => p.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Player == null)
            {
                return Problem("Entity set 'webapp2Context.Player'  is null.");
            }
            var player = await _context.Player.FindAsync(id);
            if (player != null)
            {
                _context.Player.Remove(player);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
          return (_context.Player?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
