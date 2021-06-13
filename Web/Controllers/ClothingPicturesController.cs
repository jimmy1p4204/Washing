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
    [Authorize(Roles = "Maganer,Employee")]
    public class ClothingPicturesController : Controller
    {
        private readonly WashingDbContext _context;

        public ClothingPicturesController(WashingDbContext context)
        {
            _context = context;
        }

        // GET: ClothingPictures
        public async Task<IActionResult> Index(int clothingId)
        {
            ViewBag.ClothingId = clothingId;
            IEnumerable<ClothingPicture> model;
            if (clothingId != 0)
            {
                model = await _context.ClothingPictures.Where(x => x.ClothingId == clothingId).ToListAsync();
            }
            else 
            {
                model = await _context.ClothingPictures.ToListAsync();
            }

            return View(model);
        }

        // GET: ClothingPictures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingPicture = await _context.ClothingPictures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothingPicture == null)
            {
                return NotFound();
            }

            return View(clothingPicture);
        }


        /// <summary>
        /// 建立衣物照片
        /// </summary>
        /// <param name="clothingId"></param>
        /// <returns></returns>
        public IActionResult Create(int clothingId)
        {
            var model = new ClothingPicture() { 
                ClothingId = clothingId
            };
            return View(model);
        }

        
        /// <summary>
        /// 建立衣物照片
        /// </summary>
        /// <param name="clothingPicture"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClothingPicture clothingPicture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clothingPicture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { ClothingId = clothingPicture.ClothingId});
            }
            return View(clothingPicture);
        }

        // GET: ClothingPictures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingPicture = await _context.ClothingPictures.FindAsync(id);
            if (clothingPicture == null)
            {
                return NotFound();
            }
            return View(clothingPicture);
        }

        // POST: ClothingPictures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClothingId,Content,CreateDt")] ClothingPicture clothingPicture)
        {
            if (id != clothingPicture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clothingPicture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClothingPictureExists(clothingPicture.Id))
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
            return View(clothingPicture);
        }

        // 刪除照片
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothingPicture = await _context.ClothingPictures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothingPicture == null)
            {
                return NotFound();
            }

            _context.ClothingPictures.Remove(clothingPicture);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { ClothingId = clothingPicture.ClothingId });
        }
       
        private bool ClothingPictureExists(int id)
        {
            return _context.ClothingPictures.Any(e => e.Id == id);
        }
    }
}
