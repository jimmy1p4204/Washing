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
		/// 本月儲值金額
		/// </summary>
		[DisplayName("本月儲值金額")] 
		public string ThisMonthStoreAmount { get; set; }

		/// <summary>
		/// 本月收件數
		/// </summary>
		[DisplayName("本月收件數")] 
		public string ThisMonthClothings { get; set; }
	}

	/// <summary>
	/// 每月報表
	/// </summary>
	public class MonthlyReport 
	{

	}
}
