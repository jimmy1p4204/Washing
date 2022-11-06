using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
	/// <summary>
	/// 機器現金 
	/// </summary>
	public class MachineCash
	{
		/// <summary>
		/// 金額
		/// </summary>
		[DisplayName("金額")]
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
		/// 金額
		/// </summary>
		[DisplayName("金額")]
		public int Amount { get; set; }

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
