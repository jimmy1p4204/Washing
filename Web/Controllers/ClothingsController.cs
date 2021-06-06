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
    public class ClothingsController : Controller
    {
        private readonly WashingDbContext _context;

        public ClothingsController(WashingDbContext context)
        {
            _context = context;
        }

        // GET: Clothings
        public async Task<IActionResult> Index(int memberId)
        {
            if (memberId == 0)
            {
                return View(await _context.Clothings.ToListAsync());
            }
            else 
            {
                if (_context.Members.Any(x => x.Id == memberId)) {
                    var member = _context.Members.FirstOrDefault(x => x.Id == memberId);
                    ViewBag.MemberId = member.Id;
                    ViewBag.MemberName = member.Name;
                }
               
                return View(await _context.Clothings.Where(x=>x.MemberId == memberId).ToListAsync());
            }
            
        }

        // GET: Clothings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothing = await _context.Clothings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothing == null)
            {
                return NotFound();
            }

            return View(clothing);
        }

        // GET: Clothings/Create
        public IActionResult Create(int memberId)
        {
            SetMemberIdSelectList(memberId);

            var clothing = new Clothing() { 
                PickupDt = DateTime.Now,
            };
            return View(clothing);
        }

		private void SetMemberIdSelectList(int memberId)
		{
            //var selectList = new List<SelectListItem>(); 範例
            //{
            //    new SelectListItem {Text="text-1", Value="value-1" },
            //    new SelectListItem {Text="text-2", Value="value-2" },
            //    new SelectListItem {Text="text-3", Value="value-3" },
            //};
            var selectList = _context.Members.Select(x => new SelectListItem { Text = $"{x.Id} {x.Name}", Value = x.Id.ToString() });
            if (selectList.Any(x => x.Value == memberId.ToString()))
            {
                selectList.Where(x => x.Value == memberId.ToString()).FirstOrDefault().Selected = true;
            }

            ViewBag.MemberId = selectList;
        }

		// POST: Clothings/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Clothing clothing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clothing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {memberId = clothing.MemberId });
            }
            return View(clothing);
        }

        // GET: Clothings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothing = await _context.Clothings.FindAsync(id);
            if (clothing == null)
            {
                return NotFound();
            }

            SetMemberIdSelectList(clothing.MemberId);

            return View(clothing);
        }

        // POST: Clothings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Clothing clothing)
        {
            if (id != clothing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clothing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClothingExists(clothing.Id))
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
            return View(clothing);
        }

        // GET: Clothings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothing = await _context.Clothings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothing == null)
            {
                return NotFound();
            }

            return View(clothing);
        }

        // POST: Clothings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clothing = await _context.Clothings.FindAsync(id);
            _context.Clothings.Remove(clothing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClothingExists(int id)
        {
            return _context.Clothings.Any(e => e.Id == id);
        }
    }
}
