using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	public class Clothing
	{
		/// <summary>
		/// 衣物編號
		/// </summary>
		[DisplayName("編號")]
		public int Id { get; set; }

		/// <summary>
		/// 衣物編號
		/// </summary>
		[DisplayName("衣物編號")]
		[Range(0, int.MaxValue, ErrorMessage = "數值範圍須小於 2147483647")]
		public int? Seq { get; set; }

		/// <summary>
		/// 衣物對應的會員編號
		/// </summary>
		[DisplayName("會員編號")]
		public int MemberId { get; set; }

		/// <summary>
		/// 衣服類型
		/// </summary>
		[DisplayName("衣物類型")]
		public int Type { get; set; }

		/// <summary>
		/// 衣服顏色
		/// </summary>
		[DisplayName("顏色")]
		public string Color { get; set; }

		/// <summary>
		/// 清洗方式(水洗或乾洗)
		/// </summary>
		[DisplayName("清洗方式")]
		public int Action { get; set; }

		/// <summary>
		/// 衣服附註
		/// </summary>
		[DisplayName("附註")]
		public string Remark { get; set; }

		/// <summary>
		/// 衣服單價
		/// </summary>
		[DisplayName("單價")]
		public int Amount { get; set; }

		/// <summary>
		/// 折讓金額
		/// </summary>
		[DisplayName("折讓金額")]
		public int DiscountAmount { get; set; }

		/// <summary>
		/// 衣物清洗狀態 (預設 1:未清洗)
		/// <para>2:已清洗, 3:已退件</para>
		/// </summary>
		[DisplayName("狀態")]
		public ClothingStatusEnum Status { get; set; } = ClothingStatusEnum.Unwashed;

		/// <summary>
		/// 是否已付款
		/// </summary>
		[DisplayName("付款狀態")]
		public bool Paid { get; set; } = false;

		/// <summary>
		/// 顧客是否已取件
		/// </summary>
		[DisplayName("顧客是否已取件")]
		public bool IsPickup { get; set; } = false;

		/// <summary>
		/// 收件日期
		/// </summary>
		[DisplayName("收件日期")]
		[DataType(DataType.DateTime)]
		[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
		public DateTime ReceiveDt { get; set; } = DateTime.Now;

		/// <summary>
		/// 取件日期
		/// </summary>
		[DisplayName("顧客取件日期")]
		[DataType(DataType.DateTime)]
		[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
		public DateTime? PickupDt { get; set; }


		/// <summary>
		/// 入庫日期 (送洗回來後，狀態改成已清洗的時間點)
		/// </summary>
		[DisplayName("入庫日期")]
		[DataType(DataType.DateTime)]
		[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
		public DateTime? StockInTime { get; set; }

		
		/// <summary>
		/// 包裝方式
		/// </summary>
		[DisplayName("包裝方式")]
		public int? PackageTypeId { get; set; }
	}

	public enum ClothingStatusEnum
	{
		Unwashed = 1,   // 未清洗
		Washed = 2,     // 已清洗
		Returned = 3    // 退件
	}
}
