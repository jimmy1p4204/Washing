using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class ClothingActionsController : Controller
    {
        private readonly WashingDbContext _context;

        public ClothingActionsController(WashingDbContext context)
        {
            _context = context;
        }

        // GET: ClothingActions
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClothingActions.ToListAsync());
        }

        // GET: ClothingActions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingAction = await _context.ClothingActions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothingAction == null)
            {
                return NotFound();
            }

            return View(clothingAction);
        }

        // GET: ClothingActions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClothingActions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ClothingAction clothingAction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clothingAction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clothingAction);
        }

        // GET: ClothingActions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingAction = await _context.ClothingActions.FindAsync(id);
            if (clothingAction == null)
            {
                return NotFound();
            }
            return View(clothingAction);
        }

        // POST: ClothingActions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ClothingAction clothingAction)
        {
            if (id != clothingAction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clothingAction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClothingActionExists(clothingAction.Id))
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
            return View(clothingAction);
        }

        // GET: ClothingActions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingAction = await _context.ClothingActions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothingAction == null)
            {
                return NotFound();
            }

            return View(clothingAction);
        }

        // POST: ClothingActions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clothingAction = await _context.ClothingActions.FindAsync(id);
            _context.ClothingActions.Remove(clothingAction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClothingActionExists(int id)
        {
            return _context.ClothingActions.Any(e => e.Id == id);
        }
    }
}
