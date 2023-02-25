using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
	[Authorize(Roles = "Manager")]
	public class StoreReportController : Controller
	{
		private readonly WashingDbContext _context;

		public StoreReportController(WashingDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// 店鋪報表
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			var viewModel = new StoreReportModel();

			// 儲值金總餘額
			viewModel.TotalBalanceOfStoreAmount = GetTotalBalanceOfStoreAmount();

			// 餘額不足總額
			viewModel.InsufficientBalanceTotalAmount = GetInsufficientBalanceTotalAmount();

			// 本月收件數
			viewModel.ThisMonthClothings = GetThisMonthClothings();

			// 本月收件總金額
			viewModel.ThisMonthClothingsAmount = GetThisMonthClothingsAmount();
			
			// 本月收款金額
			viewModel.ThisMonthDepositAmount = GetThisMonthDepositAmount();
			
			// 本月儲值總額
			viewModel.ThisMonthStoreAmount = GetThisMonthStoreAmount();

			// 本月機器現金
			viewModel.ThisMonthMachineCash = GetThisMonthMachineCash();

			// 本日收件數
			viewModel.TodayClothings = GetTodayClothings();

			// 本日收件總金額
			viewModel.TodayClothingsAmount = GetTodayClothingsAmount();

			// 本日收款金額
			viewModel.TodayDepositAmount = GetTodayDepositAmount();

			// 本日儲值總額
			viewModel.TodayStoreAmount = GetTodayStoreAmount();

			// 本日機器現金
			viewModel.TodayMachineCash = GetTodayMachineCash();

			return View(viewModel);
		}

		



		/// <summary>
		/// 本日收件總金額
		/// </summary>
		/// <returns></returns>
		private string GetTodayClothingsAmount()
		{
			return $"{ _context.Clothings.Where(x => x.ReceiveDt >= DateTime.Today).Sum(x => x.Amount).ToString("#,#")} 元";
		}

		/// <summary>
		/// 本月收件總金額
		/// </summary>
		/// <returns></returns>
		private string GetThisMonthClothingsAmount()
		{
			return $"{ _context.Clothings.Where(x => x.ReceiveDt.Year == DateTime.Now.Year && x.ReceiveDt.Month == DateTime.Now.Month).Sum(x => x.Amount).ToString("#,#")} 元";
		}
		
		/// <summary>
		/// 本日收現金額
		/// </summary>
		/// <returns></returns>
		private string GetTodayDepositAmount()
		{
			return $"{_context.Logs.Where(x => x.LogDt >= DateTime.Today && x.Act == LogAct.儲值).Sum(y => y.Amount).ToString("#,#")} 元";
		}

		/// <summary>
		/// 本日儲值總額
		/// </summary>
		/// <returns></returns>
		private string GetTodayStoreAmount()
		{
			return $"{_context.Logs.Where(x => x.LogDt >= DateTime.Today && x.Act == LogAct.儲值).Sum(y => y.Amount +y.BonusAmount).ToString("#,#")} 元";
		}

		/// <summary>
		/// 本日收件數
		/// </summary>
		/// <returns></returns>
		private string GetTodayClothings()
		{
			return $"{_context.Clothings.Count(x => x.ReceiveDt >= DateTime.Today).ToString("#,#")} 件";
		}

		/// <summary>
		/// 年報表(18個月)
		/// </summary>
		/// <returns></returns>
		public IActionResult YearReport()
		{
			var totalMonth = 18; //總月數
			var viewModel = new ConcurrentBag<ReportModel>();
			

			for (int i = 0; i < totalMonth; i++)
			{
				var month = DateTime.Now.AddMonths(-i);
				var monthLastYear = month.AddYears(-1);
				var item = new ReportModel()
				{
					Date = month,
					DateStr = month.ToString("yyyy-MM"),
					DepositAmount = _context.Logs.Where(x => x.Act == LogAct.儲值 && x.LogDt.Year == month.Year && x.LogDt.Month == month.Month).Sum(x => x.Amount).ToString("#,#"),
					StoreAmount = _context.Logs.Where(x => x.Act == LogAct.儲值 && x.LogDt.Year == month.Year && x.LogDt.Month == month.Month).Sum(x => x.Amount + x.BonusAmount).ToString("#,#"),
					Clothings = _context.Clothings.Count(x => x.ReceiveDt.Year == month.Year && x.ReceiveDt.Month == month.Month).ToString("#,#"),
					ClothingsAmount = _context.Clothings.Where(x => x.ReceiveDt.Year == month.Year && x.ReceiveDt.Month == month.Month).Sum(x=> x.Amount),
					ClothingsAmountLastYear = _context.Clothings.Where(x => x.ReceiveDt.Year == monthLastYear.Year && x.ReceiveDt.Month == monthLastYear.Month).Sum(x => x.Amount),
					MachineCash = _context.MachineCashs.Where(x=>x.Dt.Year == month.Year && x.Dt.Month == month.Month).Sum(x => x.Amount).ToString("#,#"),
				};
				viewModel.Add(item);
			}

			ViewData["Title"] = $"年報表({totalMonth}個月)";
			ViewData["DateFormat"] = "yyyy-MM";
			ViewData["ShowChart"] = true;
			return View("Report", viewModel);
		}

		/// <summary>
		/// 月報表(90日)
		/// </summary>
		/// <returns></returns>
		public IActionResult MonthReport()
		{
			var viewModel = new ConcurrentBag<ReportModel>();

			for (int i = 0; i < 90; i++)
			{
				var day = DateTime.Now.AddDays(-i).Date;
				var dayLastYear = day.AddYears(-1);
				var item = new ReportModel()
				{
					Date = day,
					DateStr = day.ToString("yyyy-MM-dd"),
					DepositAmount = _context.Logs.Where(x => x.Act == LogAct.儲值 && x.LogDt.Date == day).Sum(x => x.Amount).ToString("#,#"),
					StoreAmount = _context.Logs.Where(x => x.Act == LogAct.儲值 && x.LogDt.Date == day).Sum(x => x.Amount + x.BonusAmount).ToString("#,#"),
					Clothings = _context.Clothings.Count(x => x.ReceiveDt.Date == day).ToString("#,#"),
					ClothingsAmount = _context.Clothings.Where(x => x.ReceiveDt.Date == day).Sum(x => x.Amount),
					ClothingsAmountLastYear = _context.Clothings.Where(x => x.ReceiveDt.Date == dayLastYear).Sum(x => x.Amount),
					MachineCash = _context.MachineCashs.Where(x => x.Dt.Date == day).Sum(x => x.Amount).ToString("#,#"),
				};
				viewModel.Add(item);
			}

			ViewData["Title"] = "月報表(90日)";
			ViewData["DateFormat"] = "MM-dd";
			return View("Report", viewModel);
		}

		/// <summary>
		/// 本月收款金額
		/// </summary>
		/// <returns></returns>
		private string GetThisMonthDepositAmount()
		{
			return $"{_context.Logs.Where(x => x.LogDt.Year == DateTime.Now.Year && x.LogDt.Month == DateTime.Now.Month && x.Act == LogAct.儲值).Sum(y => y.Amount).ToString("#,#")} 元";
		}

		/// <summary>
		/// 本月儲值總額
		/// </summary>
		/// <returns></returns>
		private string GetThisMonthStoreAmount()
		{
			return $"{_context.Logs.Where(x => x.LogDt.Year == DateTime.Now.Year && x.LogDt.Month == DateTime.Now.Month && x.Act == LogAct.儲值).Sum(y => y.Amount + y.BonusAmount).ToString("#,#")} 元";
		}

		/// <summary>
		/// 本月機器現金
		/// </summary>
		/// <returns></returns>
		private string GetThisMonthMachineCash()
		{
			return $"{_context.MachineCashs.Where(x => x.Dt.Year == DateTime.Today.Year && x.Dt.Month == DateTime.Today.Month).Sum(y => y.Amount).ToString("#,#")} 元";
		}

		/// <summary>
		/// 本月收件數
		/// </summary>
		/// <returns></returns>
		private string GetThisMonthClothings()
		{
			return $"{_context.Clothings.Count(x => x.ReceiveDt.Year == DateTime.Now.Year && x.ReceiveDt.Month == DateTime.Now.Month).ToString("#,#")} 件";
		}

		/// <summary>
		/// 取得未付衣物總餘額
		/// </summary>
		/// <returns></returns>
		private string GetInsufficientBalanceTotalAmount()
		{
			return $"{_context.Members.Where(x=>x.Amount < 0).Sum(y => y.Amount).ToString("#,#")} 元";
		}

		/// <summary>
		/// 儲值金總餘額
		/// </summary>
		/// <returns></returns>
		private string GetTotalBalanceOfStoreAmount()
		{
			return $"{_context.Members.Sum(x => x.Amount).ToString("#,#")} 元";
		}

		/// <summary>
		/// 取得今日機器現金
		/// </summary>
		/// <returns></returns>
		private string GetTodayMachineCash()
		{
			if(!_context.MachineCashs.Any(x=>x.Dt == DateTime.Today))
			{
				return  "尚未結帳";
			}
			else
			{
				return $"{_context.MachineCashs.First(x => x.Dt == DateTime.Today).Amount} 元";
			}
		}
	}
}
