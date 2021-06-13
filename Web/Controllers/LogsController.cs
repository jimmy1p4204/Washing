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
    [Authorize(Roles = "Manager")]
    public class LogsController : Controller
    {
        private readonly WashingDbContext _context;

        public LogsController(WashingDbContext context)
        {
            _context = context;
        }

        // GET: Logs
        public async Task<IActionResult> Index()
        {
            // 會員 (呈現中文用)
            ViewBag.Members = _context.Members.ToDictionary(x => x.Id, x => x);

            // 衣物類型對應 (呈現中文用)
            ViewBag.Clothings = _context.Clothings.ToDictionary(x => x.Id, x => x);

            // 衣物類型對應 (呈現中文用)
            ViewBag.ClothingTypes = _context.ClothingTypes.ToDictionary(x => x.Seq, x => x.Name);


            return View(await _context.Logs.ToListAsync());
        }

        //// GET: Logs/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var log = await _context.Logs
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (log == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(log);
        //}

        //// GET: Logs/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Logs/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Act,MemberId,Amount,Balance,ClothingSeq,Employee,LogDt")] Log log)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(log);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(log);
        //}

        //// GET: Logs/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var log = await _context.Logs.FindAsync(id);
        //    if (log == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(log);
        //}

        //// POST: Logs/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Act,MemberId,Amount,Balance,ClothingSeq,Employee,LogDt")] Log log)
        //{
        //    if (id != log.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(log);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!LogExists(log.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(log);
        //}

        //// GET: Logs/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var log = await _context.Logs
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (log == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(log);
        //}

        //// POST: Logs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var log = await _context.Logs.FindAsync(id);
        //    _context.Logs.Remove(log);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool LogExists(int id)
        //{
        //    return _context.Logs.Any(e => e.Id == id);
        //}
    }
}
