using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	/// <summary>
	/// 系統紀錄
	/// </summary>
	public class Log
	{
		/// <summary>
		/// 序號
		/// </summary>
		[DisplayName("序號")]
		public int Id { get; set; }

		/// <summary>
		/// 動作/描述
		/// </summary>
		[DisplayName("動作")]
		public string Act { get; set; }

		/// <summary>
		/// 衣物對應的會員編號
		/// </summary>
		[DisplayName("會員編號")]
		public int MemberId { get; set; }

		/// <summary>
		/// 異動金額
		/// </summary>
		[DisplayName("異動金額")]
		public int Amount { get; set; }

		/// <summary>
		/// 餘額
		/// </summary>
		[DisplayName("餘額")]
		public int Balance { get; set; }

		/// <summary>
		/// 衣物編號
		/// </summary>
		[DisplayName("衣物編號")]
		public int? ClothingId { get; set; }

		/// <summary>
		/// 員工
		/// </summary>
		[DisplayName("員工")]
		public string Employee { get; set; }

		/// <summary>
		/// 紀錄時間
		/// </summary>
		[DisplayName("時間")]
		[DataType(DataType.DateTime)]
		[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
		public DateTime LogDt { get; set; } = DateTime.Now;

		/// <summary>
		/// 優惠金額
		/// </summary>
		[DisplayName("優惠金額")]
		public int BonusAmount { get; set; }
	}

	public class LogAct
	{
		internal const string 儲值 = "儲值";
		internal const string 衣物收件 = "衣物收件";
		internal const string 衣物已付款 = "衣物已付款";
		internal const string 衣物改回未付款 = "衣物改回未付款";
		internal const string 顧客取件 = "顧客取件";
		internal const string 取消衣物取件 = "取消衣物取件";
		internal const string 衣物轉出 = "衣物轉出";
		internal const string 衣物轉入 = "衣物轉入";
		internal const string 刪除衣物 = "刪除衣物";
		internal const string 衣物退件 = "衣物退件";
		internal const string 取消衣物退件 = "取消衣物退件";
	}
}
