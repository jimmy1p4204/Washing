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
    public class ClothingPackageTypesController : Controller
    {
        private readonly WashingDbContext _context;

        public ClothingPackageTypesController(WashingDbContext context)
        {
            _context = context;
        }

        // GET: ClothingPackageTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClothingPackageTypes.ToListAsync());
        }

        // GET: ClothingPackageTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingPackageType = await _context.ClothingPackageTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothingPackageType == null)
            {
                return NotFound();
            }

            return View(clothingPackageType);
        }

        // GET: ClothingPackageTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClothingPackageTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ClothingPackageType clothingPackageType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clothingPackageType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clothingPackageType);
        }

        // GET: ClothingPackageTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingPackageType = await _context.ClothingPackageTypes.FindAsync(id);
            if (clothingPackageType == null)
            {
                return NotFound();
            }
            return View(clothingPackageType);
        }

        // POST: ClothingPackageTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ClothingPackageType clothingPackageType)
        {
            if (id != clothingPackageType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clothingPackageType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClothingPackageTypeExists(clothingPackageType.Id))
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
            return View(clothingPackageType);
        }

        // GET: ClothingPackageTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingPackageType = await _context.ClothingPackageTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothingPackageType == null)
            {
                return NotFound();
            }

            return View(clothingPackageType);
        }

        // POST: ClothingPackageTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clothingPackageType = await _context.ClothingPackageTypes.FindAsync(id);
            _context.ClothingPackageTypes.Remove(clothingPackageType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClothingPackageTypeExists(int id)
        {
            return _context.ClothingPackageTypes.Any(e => e.Id == id);
        }
    }
}
