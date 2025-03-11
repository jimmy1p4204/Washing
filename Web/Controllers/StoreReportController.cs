using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

			// 未付衣物總金額
			viewModel.UnPayAmountOfClothings = GetUnPayAmountOfClothings();

			// 本月收款金額
			viewModel.ThisMonthDepositAmount = GetThisMonthDepositAmount();
			
			// 本月儲值總額
			viewModel.ThisMonthStoreAmount = GetThisMonthStoreAmount();

			// 本月乾水洗 (不含自助洗)
			List<Clothing> thisMountClothings = GetThisMonthClothings();
			viewModel.ThisMonthClothingCount = ToCountString(thisMountClothings); // 件數
			viewModel.ThisMonthClothingAmount = ToAmountString(thisMountClothings); // 金額

			// 本月自助洗
			List<Clothing> thisMountSelfWashClothings = GetThisMonthSelfWashClothings();
			viewModel.ThisMonthSelfWashClothingCount = ToCountString(thisMountSelfWashClothings); // 件數
			viewModel.ThisMonthSelfWashClothingAmount = ToAmountString(thisMountSelfWashClothings); // 金額

			// 本月機器現金
			viewModel.ThisMonthMachineAmount = GetThisMonthMachineAmount();

			// 本月自助洗現金
			viewModel.ThisMonthSelfWashAmount = GetThisMonthSelfWashAmount();

			// 本日乾水洗 (不含自助洗)
			List<Clothing> todayClothings = thisMountClothings.Where(x => x.ReceiveDt >= DateTime.Today).ToList();
			viewModel.TodayClothingCount = ToCountString(todayClothings); // 件數
			viewModel.TodayClothingAmount = ToAmountString(todayClothings); // 金額

			// 本日自助洗
			List<Clothing> todaySelfWashClothings = thisMountSelfWashClothings.Where(x => x.ReceiveDt >= DateTime.Today).ToList();
			viewModel.TodaySelfWashClothingCount = ToCountString(todaySelfWashClothings); // 件數
			viewModel.TodaySelfWashClothingAmount = ToAmountString(todaySelfWashClothings); // 金額

			// 本日收款金額
			viewModel.TodayDepositAmount = GetTodayDepositAmount();

			// 本日儲值總額
			viewModel.TodayStoreAmount = GetTodayStoreAmount();

			// 本日機器現金
			viewModel.TodayMachineAmount = GetTodayMachineAmount();

			// 本日自助洗現金
			viewModel.TodaySelfWashAmount = GetTodaySelfWashAmount();

			return View(viewModel);
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
		/// 本月乾水洗收件數 (不含自助洗
		/// </summary>
		/// <returns></returns>
		private List<Clothing> GetThisMonthClothings()
		{
			return _context.Clothings.Where(x =>
				x.ReceiveDt.Year == DateTime.Now.Year &&
				x.ReceiveDt.Month == DateTime.Now.Month &&
				x.Type != ClothingTyoeConst.SelfWash) 
				.ToList();
		}

		/// <summary>
		/// 本月自助洗收件數
		/// </summary>
		/// <returns></returns>
		private List<Clothing> GetThisMonthSelfWashClothings()
		{
			return _context.Clothings.Where(x =>
				x.ReceiveDt.Year == DateTime.Now.Year &&
				x.ReceiveDt.Month == DateTime.Now.Month &&
				x.Type == ClothingTyoeConst.SelfWash)
				.ToList();
		}


		/// <summary>
		/// 轉成件數字串
		/// </summary>
		/// <returns></returns>
		private string ToCountString(List<Clothing> clothings)
		{
			return $"{clothings.Count().ToString("#,#")} 件";
		}

		/// <summary>
		/// 轉成金額字串
		/// </summary>
		/// <returns></returns>
		private string ToAmountString(List<Clothing> clothings)
		{
			return $"{clothings.Sum(x => x.Amount).ToString("#,#")} 元";
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

				// 今年衣物
				var totalClothings = _context.Clothings.Where(x => x.ReceiveDt.Year == month.Year && x.ReceiveDt.Month == month.Month);
				// 今年乾水洗衣物
				var clothings = totalClothings.Where(x => x.Type != ClothingTyoeConst.SelfWash);
				// 今年自助洗衣物
				var selfWashClothings = totalClothings.Where(x => x.Type == ClothingTyoeConst.SelfWash);


				// 去年衣物
				var totalClothingsLastYear = _context.Clothings.Where(x => x.ReceiveDt.Year == monthLastYear.Year && x.ReceiveDt.Month == monthLastYear.Month);
				// 去年乾水洗衣物
				var clothingsLastYear = totalClothingsLastYear.Where(x => x.Type != ClothingTyoeConst.SelfWash);
				// 去年自助洗衣物
				var selfWashClothingsLastYear = totalClothingsLastYear.Where(x => x.Type == ClothingTyoeConst.SelfWash);

				var item = new ReportModel()
				{
					Date = month,
					DateStr = month.ToString("yyyy-MM"),
					DepositAmount = _context.Logs.Where(x => x.Act == LogAct.儲值 && x.LogDt.Year == month.Year && x.LogDt.Month == month.Month).Sum(x => x.Amount),
					StoreAmount = _context.Logs.Where(x => x.Act == LogAct.儲值 && x.LogDt.Year == month.Year && x.LogDt.Month == month.Month).Sum(x => x.Amount + x.BonusAmount).ToString("#,#"),
					ClothingCount = clothings.Count().ToString("#,#"),
					ClothingAmount = clothings.Sum(x=> x.Amount),
					SelfWashClothingCount = selfWashClothings.Count().ToString("#,#"),
					SelfWashClothingAmount = selfWashClothings.Sum(x => x.Amount),
					MachineAmount = _context.CashCheckout.Where(x=>x.Dt.Year == month.Year && x.Dt.Month == month.Month).Sum(x => x.MachineAmount),
					SelfWashAmount = _context.CashCheckout.Where(x => x.Dt.Year == month.Year && x.Dt.Month == month.Month).Sum(x => x.SelfWashAmount),

					ClothingAmountLastYear = clothingsLastYear.Sum(x => x.Amount),
					SelfWashClothingAmountLastYear = selfWashClothingsLastYear.Sum(x => x.Amount),
					MachineAmountLastYear = _context.CashCheckout.Where(x => x.Dt.Year == monthLastYear.Year && x.Dt.Month == monthLastYear.Month).Sum(x => x.MachineAmount),
					SelfWashAmountLastYear = _context.CashCheckout.Where(x => x.Dt.Year == monthLastYear.Year && x.Dt.Month == monthLastYear.Month).Sum(x => x.SelfWashAmount),
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

				// 今年衣物
				var totalClothings = _context.Clothings.Where(x => x.ReceiveDt.Date == day);
				// 今年乾水洗衣物
				var clothings = totalClothings.Where(x => x.Type != ClothingTyoeConst.SelfWash);
				// 今年自助洗衣物
				var selfWashClothings = totalClothings.Where(x => x.Type == ClothingTyoeConst.SelfWash);

				// 去年衣物
				var totalClothingsLastYear = _context.Clothings.Where(x => x.ReceiveDt.Date == dayLastYear);
				// 去年乾水洗衣物
				var clothingsLastYear = totalClothingsLastYear.Where(x => x.Type != ClothingTyoeConst.SelfWash);
				// 去年自助洗衣物
				var selfWashClothingsLastYear = totalClothingsLastYear.Where(x => x.Type == ClothingTyoeConst.SelfWash);

				var item = new ReportModel()
				{
					Date = day,
					DateStr = day.ToString("yyyy-MM-dd"),
					DepositAmount = _context.Logs.Where(x => x.Act == LogAct.儲值 && x.LogDt.Date == day).Sum(x => x.Amount),
					StoreAmount = _context.Logs.Where(x => x.Act == LogAct.儲值 && x.LogDt.Date == day).Sum(x => x.Amount + x.BonusAmount).ToString("#,#"),
					ClothingCount = clothings.Count().ToString("#,#"),
					ClothingAmount = clothings.Sum(x => x.Amount),
					SelfWashClothingCount = selfWashClothings.Count().ToString("#,#"),
					SelfWashClothingAmount = selfWashClothings.Sum(x => x.Amount),
					MachineAmount = _context.CashCheckout.Where(x => x.Dt.Date == day).Sum(x => x.MachineAmount),
					SelfWashAmount = _context.CashCheckout.Where(x => x.Dt.Date == day).Sum(x => x.SelfWashAmount),

					ClothingAmountLastYear = clothingsLastYear.Sum(x => x.Amount),
					SelfWashClothingAmountLastYear = selfWashClothingsLastYear.Sum(x => x.Amount),
					MachineAmountLastYear = _context.CashCheckout.Where(x => x.Dt.Date == dayLastYear).Sum(x => x.MachineAmount),
					SelfWashAmountLastYear = _context.CashCheckout.Where(x => x.Dt.Date == dayLastYear).Sum(x => x.SelfWashAmount),
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
		private string GetThisMonthMachineAmount()
		{
			return $"{_context.CashCheckout.Where(x => x.Dt.Year == DateTime.Today.Year && x.Dt.Month == DateTime.Today.Month).Sum(y => y.MachineAmount).ToString("#,#")} 元";
		}

		/// <summary>
		/// 本月自助洗現金
		/// </summary>
		/// <returns></returns>
		private string GetThisMonthSelfWashAmount()
		{
			return $"{_context.CashCheckout.Where(x => x.Dt.Year == DateTime.Today.Year && x.Dt.Month == DateTime.Today.Month).Sum(y => y.SelfWashAmount).ToString("#,#")} 元";
		}

		

		/// <summary>
		/// 取得未付衣物總餘額
		/// </summary>
		/// <returns></returns>
		private string GetUnPayAmountOfClothings()
		{
			return $"{_context.Clothings.Where(x=>x.Paid == false && x.Status != (int)ClothingStatusEnum.Returned).Sum(y => y.Amount).ToString("#,#")} 元";
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
		private string GetTodayMachineAmount()
		{
			if(!_context.CashCheckout.Any(x=>x.Dt == DateTime.Today))
			{
				return  "尚未結帳";
			}
			else
			{
				return $"{_context.CashCheckout.First(x => x.Dt == DateTime.Today).MachineAmount} 元";
			}
		}

		/// <summary>
		/// 取得今日自助洗現金
		/// </summary>
		/// <returns></returns>
		private string GetTodaySelfWashAmount()
		{
			if (!_context.CashCheckout.Any(x => x.Dt == DateTime.Today))
			{
				return "尚未結帳";
			}
			else
			{
				return $"{_context.CashCheckout.First(x => x.Dt == DateTime.Today).SelfWashAmount} 元";
			}
		}
	}
}
