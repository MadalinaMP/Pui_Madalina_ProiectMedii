using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pui_Madalina_Proiect.Data;
using Pui_Madalina_Proiect.Models;
using Pui_Madalina_Proiect.Models.CollectionViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Pui_Madalina_Proiect.Controllers
{
    //Doar pentru utilizatorii autentificati din departamentul Sales.
    [Authorize(Policy = "OnlySales")] 
    public class CorporationsController : Controller
    {
        private readonly CollectionContext _context;

        public CorporationsController(CollectionContext context)
        {
            _context = context;
        }

        // GET: Corporations
        public async Task<IActionResult> Index(int? id, int? gameID)
        {
            var viewModel = new CorporationIndexData();
            viewModel.Corporations = await _context.Corporations
            .Include(i => i.PublishedGames)
            .ThenInclude(i => i.Game)
            .ThenInclude(i => i.Orders)
            .ThenInclude(i => i.Customer)
            .AsNoTracking()
            .OrderBy(i => i.CorporationName)
            .ToListAsync();
            if (id != null)
            {
                ViewData["CorporationID"] = id.Value;
                Corporations corporation = viewModel.Corporations.Where(
                i => i.ID == id.Value).Single();
                viewModel.Games = corporation.PublishedGames.Select(s => s.Game);
            }
            if (gameID != null)
            {
                ViewData["GameID"] = gameID.Value;
                viewModel.Orders = viewModel.Games.Where(
                x => x.ID == gameID).Single().Orders;
            }
            return View(viewModel);
        }

        // GET: Corporations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corporations = await _context.Corporations
                .FirstOrDefaultAsync(m => m.ID == id);
            if (corporations == null)
            {
                return NotFound();
            }

            return View(corporations);
        }

        // GET: Corporations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Corporations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CorporationName,Adress")] Corporations corporations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(corporations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(corporations);
        }

        // GET: Corporations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var corporation = await _context.Corporations
            .Include(i => i.PublishedGames).ThenInclude(i => i.Game)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (corporation == null)
            {
                return NotFound();
            }
            PopulatePublishedGameData(corporation);
            return View(corporation);
        }

        private void PopulatePublishedGameData(Corporations corporation)
        {
            var allGames = _context.Games;
            var publisherGames = new HashSet<int>(corporation.PublishedGames.Select(c => c.GameID));
            var viewModel = new List<PublishedGameData>();
            foreach (var game in allGames)
            {
                viewModel.Add(new PublishedGameData
                {
                    GameID = game.ID,
                    Title = game.Title,
                    IsPublished = publisherGames.Contains(game.ID)
                });
            }
            ViewData["Games"] = viewModel;
        }

        // POST: Corporations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedGames)
        {
            if (id == null)
            {
                return NotFound();
            }
            var corporationToUpdate = await _context.Corporations
            .Include(i => i.PublishedGames)
            .ThenInclude(i => i.Game)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Corporations>(
            corporationToUpdate,
            "",
            i => i.CorporationName, i => i.Adress))
            {
                UpdatePublishedGames(selectedGames, corporationToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdatePublishedGames(selectedGames, corporationToUpdate);
            PopulatePublishedGameData(corporationToUpdate);
            return View(corporationToUpdate);
        }
        private void UpdatePublishedGames(string[] selectedGames, Corporations corporationToUpdate)
        {
            if (selectedGames == null)
            {
                corporationToUpdate.PublishedGames = new List<PublishedGame>();
                return;
            }
            var selectedGamesHS = new HashSet<string>(selectedGames);
            var publishedGames = new HashSet<int>
            (corporationToUpdate.PublishedGames.Select(c => c.Game.ID));
            foreach (var game in _context.Games)
            {
                if (selectedGamesHS.Contains(game.ID.ToString()))
                {
                    if (!publishedGames.Contains(game.ID))
                    {
                        corporationToUpdate.PublishedGames.Add(new PublishedGame { CorporationID = corporationToUpdate.ID, GameID = game.ID });
                    }
                }
                else
                {
                    if (publishedGames.Contains(game.ID))
                    {
                        PublishedGame gameToRemove = corporationToUpdate.PublishedGames.FirstOrDefault(i => i.GameID == game.ID);
                        _context.Remove(gameToRemove);
                    }
                }
            }
        }

        // GET: Corporations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corporations = await _context.Corporations
                .FirstOrDefaultAsync(m => m.ID == id);
            if (corporations == null)
            {
                return NotFound();
            }

            return View(corporations);
        }

        // POST: Corporations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var corporations = await _context.Corporations.FindAsync(id);
            _context.Corporations.Remove(corporations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorporationsExists(int id)
        {
            return _context.Corporations.Any(e => e.ID == id);
        }
    }
}
