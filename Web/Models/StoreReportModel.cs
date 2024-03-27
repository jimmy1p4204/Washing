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
		/// 衣物未收總額
		/// </summary>
		[DisplayName("衣物未收總額")] 
		public string UnPayAmountOfClothings { get; set; }

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
		[DisplayName("本月機器(Cash)")]
		public string ThisMonthMachineAmount { get; set; }

		/// <summary>
		/// 本月自助洗現金總計
		/// </summary>
		[DisplayName("本月自助洗(Cash)")]
		public string ThisMonthSelfWashAmount { get; set; }

		/// <summary>
		/// 本月乾水洗(件) (不含自助洗)
		/// </summary>
		[DisplayName("本月乾水洗(件)")] 
		public string ThisMonthClothingCount { get; set; }

		/// <summary>
		/// 本月乾水洗($)
		/// </summary>
		[DisplayName("本月乾水洗($)")]
		public string ThisMonthClothingAmount { get; set; }

		/// <summary>
		/// 本月自助洗(件)
		/// </summary>
		[DisplayName("本月自助洗(件)")]
		public string ThisMonthSelfWashClothingCount { get; set; }

		/// <summary>
		/// 本月自助洗($)
		/// </summary>
		[DisplayName("本月自助洗($)")]
		public string ThisMonthSelfWashClothingAmount { get; set; }

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
		/// 今日乾水洗(件) (不含自助洗)
		/// </summary>
		[DisplayName("今日乾水洗(件)")]
		public string TodayClothingCount { get; set; }

		/// <summary>
		/// 今日乾水洗($)
		/// </summary>
		[DisplayName("今日乾水洗($)")]
		public string TodayClothingAmount { get; set; }

		/// <summary>
		/// 今日自助洗(件)
		/// </summary>
		[DisplayName("今日自助洗(件)")]
		public string TodaySelfWashClothingCount { get; set; }

		/// <summary>
		/// 今日自助洗($)
		/// </summary>
		[DisplayName("今日自助洗($)")]
		public string TodaySelfWashClothingAmount { get; set; }

		/// <summary>
		/// 今日機器現金
		/// </summary>
		[DisplayName("今日機器(Cash)")]
		public string TodayMachineAmount { get; set; }

		/// <summary>
		/// 今日自助洗現金
		/// </summary>
		[DisplayName("今日自助洗(Cash)")]
		public string TodaySelfWashAmount { get; set; }
	}

	/// <summary>
	/// 每月報表
	/// </summary>
	public class ReportModel 
	{
		/// <summary>
		/// 日期/月份
		/// </summary>
		[DisplayName("Date/Mount")]
		public DateTime Date { get; set; }

		/// <summary>
		/// 月份/日期
		/// </summary>
		[DisplayName("月份/日期")]
		public string DateStr { get; set; }

		/// <summary>
		/// 收款金額
		/// </summary>
		public int DepositAmount { get; set; }

		/// <summary>
		/// 收款金額 (千分位文字)
		/// </summary>
		[DisplayName("收款金額")]
		public string DepositAmountStr {
			get { 
				return DepositAmount.ToString("#,#"); 
			} 
		}

		/// <summary>
		/// 儲值金額
		/// </summary>
		[DisplayName("儲值金額")]
		public string StoreAmount { get; set; }

		/// <summary>
		/// 乾水洗(件) (不含自助洗)
		/// </summary>
		[DisplayName("乾水洗(件)")]
		public string ClothingCount { get; set; }

		/// <summary>
		/// 乾水洗($)
		/// </summary>
		[DisplayName("乾水洗($)")]
		public int ClothingAmount { get; set; }

		/// <summary>
		/// 自助洗(件)
		/// </summary>
		[DisplayName("自助洗(件)")]
		public string SelfWashClothingCount { get; set; }

		/// <summary>
		/// 自助洗($)
		/// </summary>
		[DisplayName("自助洗($)")]
		public int SelfWashClothingAmount { get; set; }

		/// <summary>
		/// 機器(Cash)
		/// </summary>
		[DisplayName("機器(Cash)")]
		public int MachineAmount { get; set; }

		/// <summary>
		/// 自助洗(Cash)
		/// </summary>
		[DisplayName("自助洗(Cash)")]
		public int SelfWashAmount { get; set; }


		/// <summary>
		/// 金額總計 (收款金額+機器Cash+自助洗 Cash)
		/// </summary>
		[DisplayName("金額總計(收款+Cash)")]
		//public int TotalClothingsAmount => ClothingAmount + SelfWashClothingAmount + MachineAmount + SelfWashAmount;
		public int TotalClothingsAmount => DepositAmount + MachineAmount + SelfWashAmount;

		public int ClothingAmountLastYear { get; internal set; }
		public int SelfWashClothingAmountLastYear { get; internal set; }
		public int MachineAmountLastYear { get; internal set; }
		public int SelfWashAmountLastYear { get; internal set; }

		/// <summary>
		/// 去年同期衣物收款總額
		/// </summary>
		[DisplayName("去年同期衣物收款總額")]
		public int TotalClothingAmountLastYear => ClothingAmountLastYear + SelfWashClothingAmountLastYear + MachineAmountLastYear + SelfWashAmountLastYear;

	}
}
