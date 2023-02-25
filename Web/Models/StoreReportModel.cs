using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	/// <summary>
	/// 店鋪報表
	/// </summary>
	public class StoreReportModel
	{
		/// <summary>
		/// 儲值金總餘額
		/// </summary>
		[DisplayName("儲值金總餘額")] 
		public string TotalBalanceOfStoreAmount { get; set; }

		/// <summary>
		/// 會員餘額不足總額
		/// </summary>
		[DisplayName("會員餘額不足總額")] 
		public string InsufficientBalanceTotalAmount { get; set; }

		/// <summary>
		/// 本月收款金額
		/// </summary>
		[DisplayName("本月收款金額")]
		public string ThisMonthDepositAmount { get; set; }

		/// <summary>
		/// 本月會員儲值金額(收現+優惠)
		/// </summary>
		[DisplayName("本月會員儲值金額(收現+優惠)")] 
		public string ThisMonthStoreAmount { get; set; }

		/// <summary>
		/// 本月機器現金總計
		/// </summary>
		[DisplayName("本月機器現金總計")]
		public string ThisMonthMachineCash { get; set; }

		/// <summary>
		/// 本月收件數
		/// </summary>
		[DisplayName("本月收件數")] 
		public string ThisMonthClothings { get; set; }

		/// <summary>
		/// 本月收件總金額
		/// </summary>
		[DisplayName("本月收件總金額")]
		public string ThisMonthClothingsAmount { get; set; }

		/// <summary>
		/// 今日收款金額
		/// </summary>
		[DisplayName("今日收款金額")]
		public string TodayDepositAmount { get; set; }

		/// <summary>
		/// 今日會員儲值金額(收現+優惠)
		/// </summary>
		[DisplayName("今日會員儲值金額(收現+優惠)")]
		public string TodayStoreAmount { get; set; }

		/// <summary>
		/// 今日收件數
		/// </summary>
		[DisplayName("今日收件數")]
		public string TodayClothings { get; set; }

		/// <summary>
		/// 本日收件總金額
		/// </summary>
		[DisplayName("今日收件總金額")]
		public string TodayClothingsAmount { get; set; }

		/// <summary>
		/// 今日機器現金
		/// </summary>
		[DisplayName("今日機器現金")]
		public string TodayMachineCash { get; set; }
	}

	/// <summary>
	/// 每月報表
	/// </summary>
	public class ReportModel 
	{
		/// <summary>
		/// 月份/日期
		/// </summary>
		[DisplayName("月份/日期")]
		public DateTime Date { get; set; }

		/// <summary>
		/// 月份/日期
		/// </summary>
		[DisplayName("月份/日期")]
		public string DateStr { get; set; }

		/// <summary>
		/// 收款金額
		/// </summary>
		[DisplayName("收款金額")]
		public string DepositAmount { get; set; }

		/// <summary>
		/// 儲值金額
		/// </summary>
		[DisplayName("儲值金額")]
		public string StoreAmount { get; set; }

		/// <summary>
		/// 收件數
		/// </summary>
		[DisplayName("收件數")]
		public string Clothings { get; set; }

		/// <summary>
		/// 收件總金額
		/// </summary>
		[DisplayName("收件總金額")]
		public decimal ClothingsAmount { get; set; }

		/// <summary>
		/// 收件總金額
		/// </summary>
		[DisplayName("收件總金額")]
		public string  ClothingsAmountStr => ClothingsAmount.ToString("#,#");

		/// <summary>
		/// 機器現金總額
		/// </summary>
		[DisplayName("機器現金總額")]
		public string MachineCash { get; set; }

		/// <summary>
		/// 去年同期衣物收款總額
		/// </summary>
		[DisplayName("去年同期衣物收款總額")]
		public int ClothingsAmountLastYear { get; internal set; }
	}
}
