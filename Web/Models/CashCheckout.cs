using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
	/// <summary>
	/// 現金結帳 
	/// </summary>
	public class CashCheckout
	{
		/// <summary>
		/// 編號
		/// </summary>
		[DisplayName("編號")]
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// 結帳日期
		/// </summary>
		[DisplayName("結帳日期")]
		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:yyyy/MM/dd}")]
		[Index]
		public DateTime Dt { get; set; }

		/// <summary>
		/// 機器現金金額
		/// </summary>
		[DisplayName("機器現金金額")]
		public int MachineAmount { get; set; }

		/// <summary>
		/// 自助洗現金金額
		/// </summary>
		[DisplayName("自助洗現金金額")]
		public int SelfWashAmount { get; set; }

		/// <summary>
		/// 建立日期
		/// </summary>
		[DisplayName("建立日期")]
		[DataType(DataType.DateTime)]
		[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
		public DateTime CreateDt { get; set; }

		/// <summary>
		/// 更新日期
		/// </summary>
		[DisplayName("更新日期")]
		[DataType(DataType.DateTime)]
		[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
		public DateTime UpdateDt { get; set; }

		/// <summary>
		/// 更新者
		/// </summary>
		[DisplayName("更新者")]
		public string UpdateBy { get; set; }

	}
}
