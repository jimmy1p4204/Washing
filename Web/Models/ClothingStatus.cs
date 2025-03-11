using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	/// <summary>
	/// 衣服顏色
	/// </summary>
	public class ClothingStatus
	{
		/// <summary>
		/// DB 序號
		/// </summary>
		[DisplayName("序號")]
		public ClothingStatusEnum Id { get; set; }

		/// <summary>
		/// 狀態名稱
		/// </summary>
		[DisplayName("狀態名稱")]
		public string Name { get; set; }
	}
}
