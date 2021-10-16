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
    public class ClothingTypesController : Controller
    {
        private readonly WashingDbContext _context;

        public ClothingTypesController(WashingDbContext context)
        {
            _context = context;
        }

        // GET: ClothingTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClothingTypes.ToListAsync());
        }

        // GET: ClothingTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingType = await _context.ClothingTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothingType == null)
            {
                return NotFound();
            }

            return View(clothingType);
        }

        // GET: ClothingTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClothingTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClothingType clothingType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clothingType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clothingType);
        }

        // GET: ClothingTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingType = await _context.ClothingTypes.FindAsync(id);
            if (clothingType == null)
            {
                return NotFound();
            }
            return View(clothingType);
        }

        // POST: ClothingTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClothingType clothingType)
        {
            if (id != clothingType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clothingType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClothingTypeExists(clothingType.Id))
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
            return View(clothingType);
        }

        // GET: ClothingTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingType = await _context.ClothingTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothingType == null)
            {
                return NotFound();
            }

            return View(clothingType);
        }

        // POST: ClothingTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clothingType = await _context.ClothingTypes.FindAsync(id);
            _context.ClothingTypes.Remove(clothingType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClothingTypeExists(int id)
        {
            return _context.ClothingTypes.Any(e => e.Id == id);
        }
    }
}
