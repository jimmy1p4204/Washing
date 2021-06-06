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
			List<Clothing> clothings;
			if (memberId == 0)
			{
				clothings = await _context.Clothings.ToListAsync();

			}
			else
			{
				if (_context.Members.Any(x => x.Id == memberId))
				{
					var member = _context.Members.FirstOrDefault(x => x.Id == memberId);
					ViewBag.MemberId = member.Id;
					ViewBag.MemberName = member.Name;
				}

				clothings = await _context.Clothings.Where(x => x.MemberId == memberId).ToListAsync();
			}

			// 衣物類型對應 (呈現中文用)
			ViewBag.ClothingTypes = _context.ClothingTypes.ToDictionary(x => x.Id, x => x.Spec);

			// 衣物類型對應 (呈現中文用)
			ViewBag.ClothingStatus = _context.ClothingStatus.ToDictionary(x => x.Id, x => x.Name);

			// 衣物類型對應 (呈現中文用)
			ViewBag.ClothingActions = _context.ClothingActions.ToDictionary(x => x.Id, x => x.Name);

			// 衣物類型對應 (呈現中文用)
			ViewBag.Members = _context.Members.ToDictionary(x => x.Id, x => x);

			// 將顏色編號轉成顏色
			var colors = _context.ClothingColors.ToDictionary(x => x.Id, x => x.Name);
			clothings.ForEach(x => 
			{ 
				var colorIds = x.Color.Split(',');
				x.Color = string.Join(",", colorIds.Select(y => colors[int.Parse(y)]));
			});
			return View(clothings);

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

			// 衣物類型對應 (呈現中文用)
			ViewBag.ClothingTypes = _context.ClothingTypes.ToDictionary(x => x.Id, x => x.Spec);
			// 衣物類型對應 (呈現中文用)
			ViewBag.ClothingStatus = _context.ClothingStatus.ToDictionary(x => x.Id, x => x.Name);
			// 衣物類型對應 (呈現中文用)
			ViewBag.Members = _context.Members.ToDictionary(x => x.Id, x => x);
			// 衣物類型對應 (呈現中文用)
			ViewBag.ClothingActions = _context.ClothingActions.ToDictionary(x => x.Id, x => x.Name);

			// 將顏色編號轉成顏色
			var colors = _context.ClothingColors.ToDictionary(x => x.Id, x => x.Name);
			var colorIds = clothing.Color.Split(',');
			clothing.Color = string.Join(",", colorIds.Select(y => colors[int.Parse(y)]));
			

			return View(clothing);
		}

		// GET: Clothings/Create
		public IActionResult Create(int memberId)
		{
			SetMemberIdSelectList(memberId);
			SetCloseingTypeSelectList();
			SetCloseingActionSelectList();

			var clothing = new ClothingEditViewModel()
			{
				ReceiveDt = DateTime.Now,
				Colors = new MultiSelectList(_context.ClothingColors.ToList(), "Id", "Name"),
			};
			return View(clothing);
		}

		



		// POST: Clothings/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ClothingEditViewModel clothing)
		{
			if (ModelState.IsValid)
			{
				if (clothing.SelectedColorIds.Count == 0) 
				{
					clothing.SelectedColorIds.Add(1); // 無指定
				}
				clothing.Color = string.Join(",", clothing.SelectedColorIds);
				_context.Add(clothing);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index), new { memberId = clothing.MemberId });
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
			SetCloseingTypeSelectList();
			SetCloseingStatusSelectList(clothing.Status);
			SetCloseingActionSelectList(clothing.Action);
			ViewBag.Colors = new MultiSelectList(_context.ClothingColors.ToList(), "Id", "Name", clothing.Color.Split(','));
			return View(clothing);
		}

		// POST: Clothings/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Clothing clothing, int[] selectedColorIds)
		{
			if (id != clothing.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					if (selectedColorIds.Length == 0)
					{
						selectedColorIds = new int[] { 1 }; // 無指定
					}
					clothing.Color = string.Join(',', selectedColorIds);
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
				// 找到會員
				var member = await _context.Members.FindAsync(clothing.MemberId);

				// 回到該會員的衣物清單
				return RedirectToAction(nameof(Index), new { memberId = member.Id });
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

			// 衣物類型對應 (呈現中文用)
			ViewBag.ClothingTypes = _context.ClothingTypes.ToDictionary(x => x.Id, x => x.Spec);
			// 衣物類型對應 (呈現中文用)
			ViewBag.ClothingStatus = _context.ClothingStatus.ToDictionary(x => x.Id, x => x.Name);
			// 衣物類型對應 (呈現中文用)
			ViewBag.ClothingActions = _context.ClothingActions.ToDictionary(x => x.Id, x => x.Name);

			// 將顏色編號轉成顏色
			var colors = _context.ClothingColors.ToDictionary(x => x.Id, x => x.Name);
			var colorIds = clothing.Color.Split(',');
			clothing.Color = string.Join(",", colorIds.Select(y => colors[int.Parse(y)]));

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

		/// <summary>
		/// 已付款
		/// GET: Clothings/Edit/5
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<IActionResult> Paid(int? id)
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

			// 找到會員
			var member = await _context.Members.FindAsync(clothing.MemberId);

			// 會員總額扣掉衣物餘額
			member.Amount -= clothing.Amount;

			// 改成已付款
			clothing.Paid = true;

			_context.Update(clothing);
			_context.Update(member);
			await _context.SaveChangesAsync();

			// 回到該會員的衣物清單
			return RedirectToAction(nameof(Index), new { memberId = member.Id });
		}

		//
		/// <summary>
		/// 改回未付款
		///  GET: Clothings/UnPaid/5
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<IActionResult> UnPaid(int? id)
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

			// 找到會員
			var member = await _context.Members.FindAsync(clothing.MemberId);

			// 會員總額扣掉衣物餘額
			member.Amount += clothing.Amount;

			// 改成已付款
			clothing.Paid = false;

			_context.Update(clothing);
			_context.Update(member);
			await _context.SaveChangesAsync();

			// 回到該會員的衣物清單
			return RedirectToAction(nameof(Index), new { memberId = member.Id });
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

			ViewBag.Members = selectList;
		}

		private void SetCloseingTypeSelectList(int clothingTypeId = 0)
		{
			var selectList = _context.ClothingTypes.Select(x => new SelectListItem { Text = $"({x.Seq}){x.Spec}(乾洗:{x.WashingPrice}, 水洗:{x.DryCleaningPrice})", Value = x.Id.ToString() });
			if (selectList.Any(x => x.Value == clothingTypeId.ToString()))
			{
				selectList.Where(x => x.Value == clothingTypeId.ToString()).FirstOrDefault().Selected = true;
			}

			ViewBag.ClothingTypes = selectList;
		}

		private void SetCloseingActionSelectList(int clothingActionId = 0)
		{
			var selectList = _context.ClothingActions.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
			if (selectList.Any(x => x.Value == clothingActionId.ToString()))
			{
				selectList.Where(x => x.Value == clothingActionId.ToString()).FirstOrDefault().Selected = true;
			}

			ViewBag.ClothingActions = selectList;
		}

		private void SetCloseingStatusSelectList(int clothingStatusId = 0)
		{
			var selectList = _context.ClothingStatus.Select(x => new SelectListItem { Text = $"{x.Id}:{x.Name}", Value = x.Id.ToString() });
			if (selectList.Any(x => x.Value == clothingStatusId.ToString()))
			{
				selectList.Where(x => x.Value == clothingStatusId.ToString()).FirstOrDefault().Selected = true;
			}

			ViewBag.ClothingStatus = selectList;
		}

		public class CloseingColorList
		{
			// this may have to be a List<SelectListItems> to work with MultiSelectList - check.
			public SelectList Colors { get; set; }
			public List<int> SelectedColorIds { get; set; }
		}
	}
}
