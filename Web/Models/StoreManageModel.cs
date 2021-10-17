using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	/// <summary>
	/// 未清洗衣物
	/// </summary>
	public class UnWashViewModel
	{
		public int Id { get; set; }

		/// <summary>
		/// 衣物編號
		/// </summary>
		[DisplayName("衣物編號")] 
		public int? ClothingSeq { get; set; }

		/// <summary>
		/// 已收件天數
		/// </summary>
		[DisplayName("已收件天數")] 
		public string ReceiveDayCount { get; set; }

		/// <summary>
		/// 收件日期
		/// </summary>
		[DisplayName("收件日期")]
		[DataType(DataType.DateTime)]
		[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
		public DateTime ReceiveDt { get; set; }

		/// <summary>
		/// 衣物狀態
		/// </summary>
		[DisplayName("衣物狀態")]
		public int Status { get; set; }

	}

}
