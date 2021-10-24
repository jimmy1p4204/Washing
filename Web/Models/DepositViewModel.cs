using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	/// <summary>
	/// 會員儲值 ViewModel
	/// </summary>
	public class DepositViewModel
	{
		/// <summary>
		/// 顧客編號
		/// </summary>
		[DisplayName("編號")]
		public int Id { get; set; }

		/// <summary>
		/// 會員姓名
		/// </summary>
		[DisplayName("姓名")]
		public string Name { get; set; }

		/// <summary>
		/// 儲值金額
		/// </summary>
		[DisplayName("儲值金額")]
		public int DepositAmount { get; set; } = 0;

		/// <summary>
		/// 優惠金額
		/// </summary>
		[DisplayName("優惠金額")]
		public int BonusAmount { get; set; } = 0;
	}
}
