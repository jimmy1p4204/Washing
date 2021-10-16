using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Manager,Employee")]
    public class ClothingColorsController : Controller
    {
        private readonly WashingDbContext _context;

        public ClothingColorsController(WashingDbContext context)
        {
            _context = context;
        }

        // GET: ClothingColors
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClothingColors.ToListAsync());
        }

        // GET: ClothingColors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingColor = await _context.ClothingColors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothingColor == null)
            {
                return NotFound();
            }

            return View(clothingColor);
        }

        // GET: ClothingColors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClothingColors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClothingColor clothingColor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clothingColor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clothingColor);
        }

        // GET: ClothingColors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingColor = await _context.ClothingColors.FindAsync(id);
            if (clothingColor == null)
            {
                return NotFound();
            }
            return View(clothingColor);
        }

        // POST: ClothingColors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClothingColor clothingColor)
        {
            if (id != clothingColor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clothingColor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClothingColorExists(clothingColor.Id))
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
            return View(clothingColor);
        }

        // GET: ClothingColors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingColor = await _context.ClothingColors.FirstOrDefaultAsync(m => m.Id == id);
            if (clothingColor == null)
            {
                return NotFound();
            }

            return View(clothingColor);
        }

        // POST: ClothingColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clothingColor = await _context.ClothingColors.FindAsync(id);
            _context.ClothingColors.Remove(clothingColor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClothingColorExists(int id)
        {
            return _context.ClothingColors.Any(e => e.Id == id);
        }
    }
}
