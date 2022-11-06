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
    /// <summary>
    /// 機器現金
    /// </summary>
    public class MachineCashesController : Controller
    {
        private readonly WashingDbContext _context;

        public MachineCashesController(WashingDbContext context)
        {
            _context = context;
        }

        // GET: MachineCashes
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.MachineCashs.ToListAsync());
        }

        // GET: MachineCashes/Create
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            MachineCash machineCash = new MachineCash()
            {
                Dt = DateTime.Now,
                UpdateBy = User.Identity.Name,
            };
            return View(machineCash);
        }

        // POST: MachineCashes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create([Bind("Id,Dt,Amount,CreateDt,UpdateDt,UpdateBy")] MachineCash machineCash)
        {
            if (ModelState.IsValid)
            {
                machineCash.CreateDt = DateTime.Now;
                machineCash.UpdateDt = DateTime.Now;
                _context.Add(machineCash);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(machineCash);
        }


        /// <summary>
        /// 機器現金結帳 (for 員工)
        /// GET: MachineCashes/CheckOut
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Manager,Employee,SystemManager")]
        public IActionResult CheckOut()
        {
            if (!_context.MachineCashs.Any(x => x.Dt == DateTime.Today))
			{
                MachineCash machineCash = new MachineCash()
                {
                    Dt = DateTime.Now,
                    CreateDt = DateTime.Now,
                    UpdateDt = DateTime.Now,
                    UpdateBy = User.Identity.Name,
                };
                return View(machineCash);
            }
			else
			{
                var todayMachineCash = _context.MachineCashs.First(x => x.Dt == DateTime.Today);
                todayMachineCash.UpdateBy = User.Identity.Name;
                return View(todayMachineCash);
            }
        }

        /// <summary>
        /// 機器現金結帳 (for 員工)
        /// POST: MachineCashes/CheckOut
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager,Employee,SystemManager")]
        public async Task<IActionResult> CheckOut(int? id, [Bind("Id,Dt,Amount,CreateDt,UpdateDt,UpdateBy")] MachineCash machineCash)
        {
            var todayMachineCash = _context.MachineCashs.FirstOrDefault(x => x.Dt == DateTime.Today);

            // 本日已有資料但員工編輯送出後的給的Id不對
            if (todayMachineCash != null && id != machineCash.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (todayMachineCash != null)
                    {
                        todayMachineCash.UpdateDt = DateTime.Now;
                        todayMachineCash.UpdateBy = User.Identity.Name;
                        todayMachineCash.Amount = machineCash.Amount;
                        _context.Update(todayMachineCash);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", "Home");
                    }
					else
					{
                        machineCash.UpdateDt = DateTime.Now;
                        _context.Add(machineCash);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachineCashExists(machineCash.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CheckOut));
            }
            return RedirectToAction("Index", "Home");

        }


        // GET: MachineCashes/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machineCash = await _context.MachineCashs.FindAsync(id);
            if (machineCash == null)
            {
                return NotFound();
            }

            machineCash.UpdateDt = DateTime.Now;
            machineCash.UpdateBy = User.Identity.Name;

            return View(machineCash);
        }

        // POST: MachineCashes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Dt,Amount,CreateDt,UpdateDt,UpdateBy")] MachineCash machineCash)
        {
            if (id != machineCash.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(machineCash);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachineCashExists(machineCash.Id))
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
            return View(machineCash);
        }

        // GET: MachineCashes/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machineCash = await _context.MachineCashs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (machineCash == null)
            {
                return NotFound();
            }

            return View(machineCash);
        }

        // POST: MachineCashes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var machineCash = await _context.MachineCashs.FindAsync(id);
            _context.MachineCashs.Remove(machineCash);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MachineCashExists(int id)
        {
            return _context.MachineCashs.Any(e => e.Id == id);
        }
    }
}
