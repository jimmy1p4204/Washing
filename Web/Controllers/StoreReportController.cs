using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

			// 本月收件數
			viewModel.ThisMonthClothings = GetThisMonthClothings();

			// 本月儲值總額
			viewModel.ThisMonthStoreAmount = GetThisMonthStoreAmount();

			return View(viewModel);
		}

		/// <summary>
		/// 本月儲值總額
		/// </summary>
		/// <returns></returns>
		private string GetThisMonthStoreAmount()
		{
			return $"{_context.Logs.Where(x => x.LogDt.Month == DateTime.Now.Month && x.Act == "儲值").Sum(y => y.Amount).ToString("#,#")} 元";
		}

		/// <summary>
		/// 本月收件數
		/// </summary>
		/// <returns></returns>
		private string GetThisMonthClothings()
		{
			return $"{_context.Clothings.Count(x => x.ReceiveDt.Month == DateTime.Now.Month).ToString("#,#")} 件";
		}

		/// <summary>
		/// 取得未付衣物總餘額
		/// </summary>
		/// <returns></returns>
		private string GetUnPayAmountOfClothings()
		{
			return $"{_context.Clothings.Where(x=>x.Paid == false).Sum(y => y.Amount).ToString("#,#")} 元";
		}

		/// <summary>
		/// 儲值金總餘額
		/// </summary>
		/// <returns></returns>
		private string GetTotalBalanceOfStoreAmount()
		{
			return $"{_context.Members.Sum(x => x.Amount).ToString("#,#")} 元";
		}
	}
}
