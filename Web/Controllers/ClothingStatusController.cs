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
    public class ClothingStatusController : Controller
    {
        private readonly WashingDbContext _context;

        public ClothingStatusController(WashingDbContext context)
        {
            _context = context;
        }

        // GET: ClothingStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClothingStatus.ToListAsync());
        }

        // GET: ClothingStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingStatus = await _context.ClothingStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothingStatus == null)
            {
                return NotFound();
            }

            return View(clothingStatus);
        }

        // GET: ClothingStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClothingStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ClothingStatus clothingStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clothingStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clothingStatus);
        }

        // GET: ClothingStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingStatus = await _context.ClothingStatus.FindAsync(id);
            if (clothingStatus == null)
            {
                return NotFound();
            }
            return View(clothingStatus);
        }

        // POST: ClothingStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ClothingStatus clothingStatus)
        {
            if (id != clothingStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clothingStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClothingStatusExists(clothingStatus.Id))
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
            return View(clothingStatus);
        }

        // GET: ClothingStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingStatus = await _context.ClothingStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothingStatus == null)
            {
                return NotFound();
            }

            return View(clothingStatus);
        }

        // POST: ClothingStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clothingStatus = await _context.ClothingStatus.FindAsync(id);
            _context.ClothingStatus.Remove(clothingStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClothingStatusExists(int id)
        {
            return _context.ClothingStatus.Any(e => e.Id == id);
        }
    }
}
