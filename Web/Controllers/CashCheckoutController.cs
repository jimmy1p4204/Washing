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
    /// 現金結帳
    /// </summary>
    public class CashCheckoutController : Controller
    {
        private readonly WashingDbContext _context;

        public CashCheckoutController(WashingDbContext context)
        {
            _context = context;
        }

        // GET: CashCheckout
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.CashCheckout.ToListAsync());
        }

        // GET: CashCheckout/Create
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            CashCheckout cashCheckout = new CashCheckout()
            {
                Dt = DateTime.Now,
                UpdateBy = User.Identity.Name,
            };
            return View(cashCheckout);
        }

        // POST: CashCheckout/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create([Bind("Id,Dt,MachineAmount,SelfWashAmount,CreateDt,UpdateDt,UpdateBy")] CashCheckout cashCheckout)
        {
            if (ModelState.IsValid)
            {
                cashCheckout.CreateDt = DateTime.Now;
                cashCheckout.UpdateDt = DateTime.Now;
                _context.Add(cashCheckout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cashCheckout);
        }


        /// <summary>
        /// 現金結帳結帳 (for 員工)
        /// GET: CashCheckout/CheckOut
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Manager,Employee,SystemManager")]
        public IActionResult CheckOut()
        {
            if (!_context.CashCheckout.Any(x => x.Dt == DateTime.Today))
			{
                CashCheckout cashCheckout = new CashCheckout()
                {
                    Dt = DateTime.Now,
                    CreateDt = DateTime.Now,
                    UpdateDt = DateTime.Now,
                    UpdateBy = User.Identity.Name,
                };
                return View(cashCheckout);
            }
			else
			{
                var todayCashCheckout = _context.CashCheckout.First(x => x.Dt == DateTime.Today);
                todayCashCheckout.UpdateBy = User.Identity.Name;
                return View(todayCashCheckout);
            }
        }

        /// <summary>
        /// 現金結帳結帳 (for 員工)
        /// POST: CashCheckout/CheckOut
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager,Employee,SystemManager")]
        public async Task<IActionResult> CheckOut(int? id, [Bind("Id,Dt,MachineAmount,SelfWashAmount,CreateDt,UpdateDt,UpdateBy")] CashCheckout cashCheckout)
        {
            var todayCashCheckout = _context.CashCheckout.FirstOrDefault(x => x.Dt == DateTime.Today);

            // 本日已有資料但員工編輯送出後的給的Id不對
            if (todayCashCheckout != null && id != cashCheckout.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (todayCashCheckout != null)
                    {
                        todayCashCheckout.UpdateDt = DateTime.Now;
                        todayCashCheckout.UpdateBy = User.Identity.Name;
                        todayCashCheckout.MachineAmount = cashCheckout.MachineAmount;
                        todayCashCheckout.SelfWashAmount = cashCheckout.SelfWashAmount;
                        _context.Update(todayCashCheckout);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", "Home");
                    }
					else
					{
                        cashCheckout.UpdateDt = DateTime.Now;
                        _context.Add(cashCheckout);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CashCheckoutExists(cashCheckout.Id))
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


        // GET: CashCheckout/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CashCheckout = await _context.CashCheckout.FindAsync(id);
            if (CashCheckout == null)
            {
                return NotFound();
            }

            CashCheckout.UpdateDt = DateTime.Now;
            CashCheckout.UpdateBy = User.Identity.Name;

            return View(CashCheckout);
        }

        // POST: CashCheckout/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Dt,MachineAmount,SelfWashAmount,CreateDt,UpdateDt,UpdateBy")] CashCheckout CashCheckout)
        {
            if (id != CashCheckout.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(CashCheckout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CashCheckoutExists(CashCheckout.Id))
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
            return View(CashCheckout);
        }

        // GET: CashCheckout/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CashCheckout = await _context.CashCheckout
                .FirstOrDefaultAsync(m => m.Id == id);
            if (CashCheckout == null)
            {
                return NotFound();
            }

            return View(CashCheckout);
        }

        // POST: CashCheckout/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var CashCheckout = await _context.CashCheckout.FindAsync(id);
            _context.CashCheckout.Remove(CashCheckout);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CashCheckoutExists(int id)
        {
            return _context.CashCheckout.Any(e => e.Id == id);
        }
    }
}
