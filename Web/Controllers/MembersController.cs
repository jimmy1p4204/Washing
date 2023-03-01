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

			ViewBag.Action = nameof(Index);
			return View(await _context.Members.ToListAsync());
		}

		// GET: Members/Search
		public IActionResult Search()
		{
			return View();
		}

		// POST: Members/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Search(SearchMemberViewModel member)
		{
			if (!ModelState.IsValid) 
			{
				return View();
			}

			IQueryable<Member> members = null;
			if (member.MemberId.HasValue && member.MemberId > 0)
			{
				members = _context.Members.Where(x => member.MemberId.ToString().Contains(x.Id.ToString()));
				if (!members.Any())
				{
					ViewBag.MemberIdErrorMsg = "會員編號不存在";
					return View();
				}
			}
			else if(!string.IsNullOrEmpty(member.Phone))
			{
				members = _context.Members.Where(x => x.Phone.Contains(member.Phone));
				if (!members.Any())
				{
					ViewBag.PhoneErrorMsg = "電話不存在";
					return View();
				}
			}
			else if (!string.IsNullOrEmpty(member.Name))
			{
				members = _context.Members.Where(x => x.Name.Contains(member.Name));
				if (!members.Any())
				{
					ViewBag.NameErrorMsg = "名字不存在";
					return View();
				}
			}
			else
			{
				return View();
			}

			ViewBag.Action = nameof(Search);
			if (members.Count() > 1)
			{
				foreach (var item in members)
				{
					var clothings = await _context.Clothings.Where(x => x.MemberId == item.Id).ToListAsync();

					// 未付衣物金額
					var unPayAmount = clothings.Where(x => x.Paid == false).Sum(x => x.Amount);

					// 預計餘額 (會員儲值餘額 - 未付衣物金額
					item.Amount -= unPayAmount;
				}
				
				return View(nameof(Index), members.ToList());
			}
			else
			{
				return RedirectToAction(nameof(Index), "Clothings", new { memberId = member.MemberId });
			}
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

			var viewModel = new DepositViewModel()
			{
				Id = member.Id,
				Name = member.Name,
			};
			
			return View(viewModel);
		}

		/// <summary>
		/// 儲值
		/// </summary>
		/// <param name="id"></param>
		/// <param name="viewModel"></param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditAmount(int id, DepositViewModel viewModel)
		{
			if (id != viewModel.Id)
			{
				return NotFound();
			}

			var member = await _context.Members.FindAsync(id);
			if (member == null)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				try
				{
					member.UpdateBy = User.Identity.Name;
					member.UpdateDt = DateTime.Now;
					member.Amount += viewModel.DepositAmount + viewModel.BonusAmount;
					_context.Update(member);
					_context.Logs.Add(new Log()
					{
						Act = LogAct.儲值,
						MemberId = id,
						Amount = viewModel.DepositAmount,
						BonusAmount = viewModel.BonusAmount,
						Balance = member.Amount,
						Employee = User.Identity.Name,
					});
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!MemberExists(viewModel.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				//儲值完回到會員衣物清單
				return RedirectToAction("Index", "Clothings", new { memberId = viewModel.Id });
			}
			return View(viewModel);
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
		[Authorize(Roles = "Manager")]
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
