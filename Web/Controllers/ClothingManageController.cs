using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
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
		/// 未清洗清單
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			var viewModel = _context.Clothings.Where(x => x.Status == 1).Select(x => new UnWashViewModel() { 
				Id = x.Id,
				ClothingSeq = x.Seq,
				ReceiveDayCount = DateTime.Now.Subtract(x.ReceiveDt).TotalDays.ToString("#,#"),
				ReceiveDt = x.ReceiveDt,
				Status = x.Status,
				PickupDt = x.PickupDt,
				Paid = x.Paid
			});

			// 衣物狀態對應 (呈現中文用)
			ViewBag.ClothingStatus = _context.ClothingStatus.ToDictionary(x => x.Id, x => x.Name);

			return View(viewModel);
		}

		/// <summary>
		/// 已清洗未取件清單
		/// </summary>
		/// <returns></returns>
		public IActionResult WashedButUnPick()
		{
			var viewModel = _context.Clothings.Where(x => x.Status == 2 && x.IsPickup == false).Select(x => new UnWashViewModel()
			{
				Id = x.Id,
				ClothingSeq = x.Seq,
				ReceiveDayCount = DateTime.Now.Subtract(x.ReceiveDt).TotalDays.ToString("#,#"),
				ReceiveDt = x.ReceiveDt,
				Status = x.Status,
				PickupDt = x.PickupDt,
				Paid = x.Paid
			});

			// 衣物狀態對應 (呈現中文用)
			ViewBag.ClothingStatus = _context.ClothingStatus.ToDictionary(x => x.Id, x => x.Name);

			return View(viewModel);
		}

		/// <summary>
		/// 已清洗未付款清單
		/// </summary>
		/// <returns></returns>
		public IActionResult PickedButUnPay()
		{
			var viewModel = _context.Clothings.Where(x => x.Paid == false && x.IsPickup).Select(x => new UnWashViewModel()
			{
				Id = x.Id,
				ClothingSeq = x.Seq,
				ReceiveDayCount = DateTime.Now.Subtract(x.ReceiveDt).TotalDays.ToString("#,#"),
				ReceiveDt = x.ReceiveDt,
				Status = x.Status,
				PickupDt = x.PickupDt,
				Paid = x.Paid
			});

			// 衣物狀態對應 (呈現中文用)
			ViewBag.ClothingStatus = _context.ClothingStatus.ToDictionary(x => x.Id, x => x.Name);

			return View(viewModel);
		}

	}
}
