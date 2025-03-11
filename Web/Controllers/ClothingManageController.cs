using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
	[Authorize(Roles = "Manager,Employee")]
	public class ClothingManageController : Controller
	{
		private readonly WashingDbContext _context;

		public ClothingManageController(WashingDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// 未付款清單 (排除退件)
		/// </summary>
		/// <returns></returns>
		public IActionResult UnPaid()
		{
			ViewData["Title"] = "未付款衣物清單";

			var clothings = _context.Clothings.Where(x => x.Paid == false && x.Status != ClothingStatusEnum.Returned);

			var viewModel = GetViewData(clothings);

			return View(nameof(Index), viewModel);
		}

		/// <summary>
		/// 未清洗清單
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			ViewData["Title"] = "未清洗衣物清單";

			var clothings = _context.Clothings.Where(x=> x.Status == ClothingStatusEnum.Unwashed);

			var viewModel = GetViewData(clothings);

			return View(nameof(Index), viewModel);
		}

		/// <summary>
		/// 已清洗未取件清單
		/// </summary>
		/// <returns></returns>
		public IActionResult WashedButUnPick()
		{
			ViewData["Title"] = "已清洗未取件衣物清單";

			var clothings = _context.Clothings.Where(x => x.Status == ClothingStatusEnum.Washed && x.IsPickup == false);

			var viewModel = GetViewData(clothings);

			return View(nameof(Index), viewModel);
		}

		/// <summary>
		/// 已取件未付款清單
		/// </summary>
		/// <returns></returns>
		public IActionResult PickedButUnPay()
		{
			ViewData["Title"] = "已取件未付款衣物清單";

			var clothings = _context.Clothings.Where(x => x.Paid == false && x.IsPickup && x.Status != ClothingStatusEnum.Returned);

			var viewModel = GetViewData(clothings);

			return View(nameof(Index), viewModel);
		}

		/// <summary>
		/// 取得相關資料 (含 ViewBag)
		/// </summary>
		private List<ClothingManageViewModel> GetViewData(IQueryable<Clothing> clothings)
		{
			var viewModel = clothings.Select(x => new ClothingManageViewModel()
			{
				Id = x.Id,
				ClothingSeq = x.Seq,
				MemberId = x.MemberId,
				Type = x.Type,
				Color = x.Color,
				Amount = x.Amount,
				ReceiveDayCount = DateTime.Now.Subtract(x.ReceiveDt).TotalDays.ToString("#,#"),
				ReceiveDt = x.ReceiveDt,
				Status = x.Status,
				PickupDt = x.PickupDt,
				Paid = x.Paid
			}).ToList();

			// 衣物狀態對應 (呈現中文用)
			ViewBag.ClothingStatus = _context.ClothingStatus.ToDictionary(x => x.Id, x => x.Name);

			// 會員
			ViewBag.Members = _context.Members.ToDictionary(x => x.Id, x => x);

			// 衣物類型對應 (呈現中文用)
			ViewBag.ClothingTypes = _context.ClothingTypes.ToDictionary(x => x.Id, x => x.Name);

			// 將顏色編號轉成顏色
			viewModel = viewModel.GetColorMapping(_context);

			// 總筆數
			ViewBag.Count = viewModel.Count();

			return viewModel;
		}
	}
}
