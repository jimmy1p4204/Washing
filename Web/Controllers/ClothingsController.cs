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
	public class ClothingsController : Controller
	{
		private readonly WashingDbContext _context;
		private readonly Dictionary<int, string> colors;

		public ClothingsController(WashingDbContext context)
		{
			_context = context;
			colors = _context.ClothingColors.ToDictionary(x => x.Id, x => x.Name);

		}

		/// <summary>
		/// 取得衣物清單
		/// </summary>
		/// <param name="memberId"></param>
		/// <param name="unPickup">預設為:僅顯示未取件</param>
		/// <returns></returns>
		public async Task<IActionResult> Index(int memberId, bool unPickup = true, bool print = false)
		{
			ViewBag.ErrorMsg = TempData["ErrorMsg"]?.ToString();

			if (memberId != 0 && !_context.Members.Any(x => x.Id == memberId))
			{
				return NotFound();
			}

			(DateTime startDate, DateTime endDate) = GetInterval(null, 0);
			IEnumerable<Clothing> clothings = GetClothings(memberId, startDate, endDate, unPickup);
			
			if (print)
			{
				// 友善列印頁
				return View("Print", clothings);
			}
			else
			{
				return View(clothings);
			}
			

		}

		public async Task<IActionResult> IndexForFactory(short monthOffset = 0, short offset=0) 
		{
			ViewBag.ErrorMsg = TempData["ErrorMsg"]?.ToString();
			ViewBag.monthOffset = monthOffset;

			(DateTime startDate, DateTime endDate) = GetInterval(monthOffset, offset);

			IEnumerable<Clothing> clothings = GetClothings(0, startDate, endDate, false, true);

			ViewData["Title"] = string.Format("乾水洗衣物清單 ({0} ~ {1})", startDate.ToShortDateString(), endDate.ToShortDateString());
			return View(clothings);
		}

		public async Task<IActionResult> IndexForStockIn(short dayOffset = 0, short offset = 0)
		{
			ViewBag.ErrorMsg = TempData["ErrorMsg"]?.ToString();
			ViewBag.dayOffset = dayOffset;

			(DateTime startDate, DateTime endDate) = GetIntervalForDay(dayOffset, offset);

			IEnumerable<Clothing> clothings = GetClothings(0, startDate, endDate, false, true, QueryTypeEnum.StockInTime);

			ViewData["Title"] = string.Format("衣物入庫清單 ({0} ~ {1})", startDate.ToShortDateString(), endDate.ToShortDateString());
			return View(clothings);
		}


		/// <summary>
		/// 根據條件取得衣物清單
		/// </summary>
		/// <param name="memberId"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="unPickup">僅顯示未取件</param>
		/// <param name="factoryWash">僅顯示乾水洗</param>
		/// <returns></returns>
		private IEnumerable<Clothing> GetClothings(int memberId, DateTime startDate, DateTime endDate, bool unPickup = true, bool factoryWash = false, QueryTypeEnum queryType = QueryTypeEnum.ReceiveDt) {
			
			IEnumerable<Clothing> clothings;

			Member member = null;
			if (memberId == 0)
			{
				clothings =  _context.Clothings.ToList();
			}
			else
			{
				if (_context.Members.Any(x => x.Id == memberId))
				{
					member = _context.Members.FirstOrDefault(x => x.Id == memberId);
					ViewBag.MemberId = member.Id;
					ViewBag.MemberName = member.Name;
					ViewBag.ReportDt = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
				}

				clothings = _context.Clothings.Where(x => x.MemberId == memberId).ToList();

				// 未付衣物金額
				var unPayAmount = clothings.Where(x => x.Paid == false).Sum(x => x.Amount);
				ViewBag.UnPayAmount = unPayAmount;
				// 預計餘額
				ViewBag.estimateAmount = member.Amount - unPayAmount;
			}

			// 預設顯示未取件
			if (unPickup)
			{
				clothings = clothings.Where(x => (x.IsPickup == false));
			}

			// 根據月份篩選條件
			switch (queryType)
			{
				case QueryTypeEnum.ReceiveDt:
					clothings = clothings.Where(x => (x.ReceiveDt >= startDate && x.ReceiveDt < endDate));
					break;
				case QueryTypeEnum.StockInTime:
					clothings = clothings.Where(x => (x.StockInTime >= startDate && x.StockInTime < endDate));
					break;
				default:
					break;
			}
			

			// 僅顯示乾水洗 (送工廠洗)
			if (factoryWash)
			{
				clothings = clothings.Where(x => x.Type != ClothingTyoeConst.SelfWash);
			}
			

			AssignViewData();

			// 將顏色編號轉成顏色
			clothings.ToList().ForEach(x =>
			{
				x.Color = ColorIdToStr(x.Color);
			});

			return clothings;
		}

		/// <summary>
		/// 取得時間區間
		/// </summary>
		/// <param name="date"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		private (DateTime startDate, DateTime endDate) GetInterval(Nullable<DateTime> date = null, short offset = 0) {

			DateTime startDate;
			DateTime endDate;

			// 根據月份篩選條件
			if (!date.HasValue && offset == 0)
			{
				return (DateTime.MinValue.Date, DateTime.MaxValue.Date);
			}

			if (!date.HasValue)
			{
				startDate = DateTime.Today;
				endDate = DateTime.Today;
			}
			else
			{
				startDate = date.Value.Date;
				endDate = date.Value.Date;
			}
			
			if (offset > 0)
			{
				endDate = endDate.AddDays(offset);
			}
			if (offset < 0)
			{
				startDate = startDate.AddDays(offset);
			}
			return (startDate, endDate);
		}


		private (DateTime startDate, DateTime endDate) GetInterval(short monthOffset = 0, short offset = 0) {
			DateTime today = DateTime.Today;
			DateTime targetMonth = today.AddMonths(monthOffset);
			DateTime startDate = new DateTime(targetMonth.Year, targetMonth.Month, 1);
			DateTime endDate = new DateTime(targetMonth.Year, targetMonth.Month, 1).AddMonths(1);

			if (offset > 0) {
				endDate = endDate.AddMonths(offset);
			}
			if(offset < 0) {
				startDate = startDate.AddMonths(offset);
			}
			return (startDate, endDate);
		}
		private (DateTime startDate, DateTime endDate) GetIntervalForDay(short dayOffset = 0, short offset = 0)
		{
			DateTime today = DateTime.Today;
			DateTime startDate = today.AddDays(dayOffset);
			DateTime endDate = startDate.AddDays(1);

			if (offset > 0)
			{
				endDate = endDate.AddDays(offset);
			}
			if (offset < 0)
			{
				startDate = startDate.AddDays(offset);
			}
			return (startDate, endDate);
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

			AssignViewData();

			// 將顏色編號轉成顏色
			clothing.Color = ColorIdToStr(clothing.Color);

			// 取得衣物照片
			ViewBag.ClothingPictures = _context.ClothingPictures.Where(x => x.ClothingId == id);
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

		



		/// <summary>
		/// 衣服收件
		/// </summary>
		/// <param name="clothing"></param>
		/// <returns></returns>
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

				// 找到會員
				var member = await _context.Members.FindAsync(clothing.MemberId);

				_context.Logs.Add(new Log()
				{
					Act = LogAct.衣物收件,
					MemberId = clothing.MemberId,
					Amount = 0,
					Balance = member.Amount,
					Employee = User.Identity.Name,
					ClothingId = clothing.Id,
				});

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Details), new { Id = clothing.Id });
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
			SetCloseingPackageTypeSelectList(clothing.PackageTypeId);
			ViewBag.Colors = new MultiSelectList(_context.ClothingColors.ToList(), "Id", "Name", clothing.Color.Split(','));
			ViewBag.OriginalClothingStatus = clothing.Status; // 記錄原始狀態
			return View(clothing);
		}

		// POST: Clothings/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int? id, ClothingEditViewModel clothing, ClothingStatusEnum OriginalClothingStatus)
		{
			if (id != clothing.Id)
			{
				return NotFound();
			}

			if (!ModelState.IsValid) {
				return View(clothing);
			}

			
			try
			{
				// 處理 ColorIds
				if (clothing.SelectedColorIds.Count == 0)
				{
					clothing.SelectedColorIds.Add(1); // 無指定
				}
				clothing.Color = string.Join(",", clothing.SelectedColorIds);

				//	處理衣物狀態，若有異動則要記錄異動時間
			   
				if (OriginalClothingStatus != clothing.Status) // 若狀態異動
				{
					switch (clothing.Status)
					{
						case ClothingStatusEnum.Unwashed:
							clothing.StockInTime = null;
							break;
						case ClothingStatusEnum.Washed:
							clothing.StockInTime = DateTime.Now; // 更新入庫時間
							break;
						default:
							break;
					}
				}

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

			AssignViewData();

			// 將顏色編號轉成顏色
			clothing.Color = ColorIdToStr(clothing.Color);

			return View(clothing);
		}

		// POST: Clothings/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Manager")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var clothing = await _context.Clothings.FindAsync(id);
			var memberId = clothing.MemberId;
			_context.Clothings.Remove(clothing);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index), new { memberId = memberId });
		}

		/// <summary>
		/// 衣物移轉
		///  GET: Clothings/Transfer/5
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<IActionResult> Transfer(int? id)
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

			AssignViewData();

			// 將顏色編號轉成顏色
			clothing.Color = ColorIdToStr(clothing.Color);

			return View(clothing);
		}

		/// <summary>
		/// 衣物移轉確認
		/// POST: Clothings/Transfer/5
		/// </summary>
		/// <param name="id">原會員</param>
		/// <param name="id">目標會員</param>
		/// <returns></returns>
		[HttpPost, ActionName("Transfer")]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Manager")]
		public async Task<IActionResult> TransferConfirmed(int id, int memberId)
		{
			var clothing = await _context.Clothings.FindAsync(id);

			if (clothing.Paid)
			{
				return RedirectToAction(nameof(Index), new { id });
			}

			if(memberId < 1)
			{
				ViewBag.MemberIdError = "請輸入正確的會員編號";
				return RedirectToAction(nameof(Index), new { id });
			}

			var originalMemberId = clothing.MemberId;

			//衣物調整: 衣物編號 原會員 -> 目標會員
			clothing.MemberId = memberId;
			_context.Update(clothing);

			//Log 調整
			_context.Logs.Add(new Log()
			{
				Act = LogAct.衣物轉出,
				MemberId = originalMemberId,
				Amount = 0,
				Balance = 0,
				ClothingId = id,
				Employee = User.Identity.Name,
			});
			_context.Logs.Add(new Log()
			{
				Act = LogAct.衣物轉入,
				MemberId = memberId,
				Amount = 0,
				Balance = 0,
				ClothingId = id,
				Employee = User.Identity.Name,
			});

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index), new { memberId = originalMemberId });
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
			member.UpdateDt = DateTime.Now;

			// 改成已付款
			clothing.Paid = true;

			_context.Update(clothing);
			_context.Update(member);

			_context.Logs.Add(new Log()
			{
				Act = LogAct.衣物已付款,
				MemberId = member.Id,
				Amount = -clothing.Amount,
				Balance = member.Amount,
				ClothingId = id,
				Employee = User.Identity.Name,
			});
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

			_context.Logs.Add(new Log()
			{
				Act = LogAct.衣物改回未付款,
				MemberId = member.Id,
				Amount = clothing.Amount,
				Balance = member.Amount,
				ClothingId = id,
				Employee = User.Identity.Name,
			});

			await _context.SaveChangesAsync();

			// 回到該會員的衣物清單
			return RedirectToAction(nameof(Index), new { memberId = member.Id });
		}

		/// <summary>
		/// 顧客取件
		/// GET: Clothings/Edit/5
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<IActionResult> Pickup(int? id)
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

			clothing.IsPickup = true;
			clothing.PickupDt = DateTime.Now;

			_context.Update(clothing);

			_context.Logs.Add(new Log()
			{
				Act = LogAct.顧客取件,
				MemberId = clothing.MemberId,
				Amount = 0,
				Balance = member.Amount,
				ClothingId = id,
				Employee = User.Identity.Name,
			});

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
		public async Task<IActionResult> UnPickup(int? id)
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

			clothing.IsPickup = false;
			clothing.PickupDt = null;

			_context.Update(clothing);

			_context.Logs.Add(new Log()
			{
				Act = LogAct.取消衣物取件,
				MemberId = clothing.MemberId,
				Amount = 0,
				Balance = member.Amount,
				ClothingId = clothing.Id,
				Employee = User.Identity.Name,
			});

			await _context.SaveChangesAsync();

			// 回到該會員的衣物清單
			return RedirectToAction(nameof(Index), new { memberId = member.Id });
		}

		/// <summary>
		/// 設定 View 所需要的資料 (ViewData、ViewBag)
		/// </summary>
		private void AssignViewData()
		{
			// 衣物類型對應 (呈現中文用)
			ViewBag.ClothingTypes = _context.ClothingTypes.ToDictionary(x => x.Id, x => x.Name);
			// 衣物類型對應 (呈現中文用)
			ViewBag.ClothingStatus = _context.ClothingStatus.ToDictionary(x => x.Id, x => x.Name);
			// 衣物類型對應 (呈現中文用)
			ViewBag.ClothingActions = _context.ClothingActions.ToDictionary(x => x.Id, x => x.Name);
			// 會員資料 (呈現中文用)
			ViewBag.Members = _context.Members.ToDictionary(x => x.Id, x => x);
			// 衣物包裝方式 (呈現中文用)
			ViewBag.ClothingPackageTypes = _context.ClothingPackageTypes.ToDictionary(x => x.Id, x => x.Name);
		}

		/// <summary>
		/// 將衣物的「顏色Id」轉成容易看的「顏色中文字」
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		private string ColorIdToStr(string color)
		{
			var colorIds = color.Split(',');
			return string.Join(",", colorIds.Select(y => colors[int.Parse(y)]));
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
			var selectList = _context.ClothingTypes.OrderBy(x=>x.Seq).Select(x => new SelectListItem { Text = $"({x.Seq}){x.Name}(乾洗:{x.DryCleaningPrice}, 水洗:{x.WashingPrice})", Value = x.Id.ToString() });
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

		private void SetCloseingPackageTypeSelectList(int? packageTypeId)
		{
			var selectList = _context.ClothingPackageTypes.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
			selectList.Add(new SelectListItem() { Text = "未指定", Value = "0" });
			if (packageTypeId != null && packageTypeId.Value != 0) 
			{
				if (selectList.Any(x => x.Value == packageTypeId.ToString()))
				{
					selectList.Where(x => x.Value == packageTypeId.ToString()).FirstOrDefault().Selected = true;
				}
			}
			

			ViewBag.ClothingPackageTypes = selectList.OrderBy(x => x.Value);
		}

		private void SetCloseingStatusSelectList(ClothingStatusEnum clothingStatusId = 0)
		{
			var selectList = _context.ClothingStatus.Select(x => new SelectListItem { Text = $"{x.Id}:{x.Name}", Value = x.Id.ToString() }).ToList();
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

	public enum QueryTypeEnum
	{
		ReceiveDt = 1,
		StockInTime = 2,
	}
}
