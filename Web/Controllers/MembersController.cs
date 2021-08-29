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
	public class MembersController : Controller
	{
		private readonly WashingDbContext _context;

		public MembersController(WashingDbContext context)
		{
			_context = context;
		}

		// GET: Members
		public async Task<IActionResult> Index(int id = 0)
		{
			if (id != 0)
			{
				if (_context.Members.Any(x => x.Id == id))
				{
					var member = _context.Members.Where(x => x.Id == id);
					return View(await member.ToListAsync());
				}
			}

			return View(await _context.Members.ToListAsync());
		}

		// GET: Members/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var member = await _context.Members
				.FirstOrDefaultAsync(m => m.Id == id);
			if (member == null)
			{
				return NotFound();
			}

			return View(member);
		}

		// GET: Members/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Members/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Member member)
		{
			if (ModelState.IsValid)
			{
				member.CreateDt = DateTime.Now;
				member.CreateBy = User.Identity.Name;

				_context.Add(member);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(member);
		}

		// GET: Members/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var member = await _context.Members.FindAsync(id);
			if (member == null)
			{
				return NotFound();
			}
			return View(member);
		}

		// POST: Members/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Member member)
		{
			if (id != member.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					member.UpdateBy = User.Identity.Name;
					member.UpdateDt = DateTime.Now;
					_context.Update(member);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!MemberExists(member.Id))
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
			return View(member);
		}

		// GET: Members/Edit/5
		public async Task<IActionResult> EditAmount(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var member = await _context.Members.FindAsync(id);
			if (member == null)
			{
				return NotFound();
			}
			member.Amount = 0;
			return View(member);
		}

		/// <summary>
		/// 儲值
		/// </summary>
		/// <param name="id"></param>
		/// <param name="member"></param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditAmount(int id, Member member)
		{
			if (id != member.Id)
			{
				return NotFound();
			}

			var memberFromDb = await _context.Members.FindAsync(id);
			if (memberFromDb == null)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				try
				{
					memberFromDb.UpdateBy = User.Identity.Name;
					memberFromDb.UpdateDt = DateTime.Now;
					memberFromDb.Amount += member.Amount;
					_context.Update(memberFromDb);
					_context.Logs.Add(new Log()
					{
						Act = "儲值",
						MemberId = id,
						Amount = member.Amount,
						Balance = memberFromDb.Amount,
						Employee = User.Identity.Name,
					});
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!MemberExists(member.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				//儲值完回到會員衣物清單
				return RedirectToAction("Index", "Clothings", new { memberId = member.Id });
			}
			return View(member);
		}


		// GET: Members/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var member = await _context.Members
				.FirstOrDefaultAsync(m => m.Id == id);
			if (member == null)
			{
				return NotFound();
			}

			return View(member);
		}

		// POST: Members/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var member = await _context.Members.FindAsync(id);
			_context.Members.Remove(member);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool MemberExists(int id)
		{
			return _context.Members.Any(e => e.Id == id);
		}
	}
}
