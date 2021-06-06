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
	public class ClothingColor
	{
		/// <summary>
		/// DB 序號
		/// </summary>
		[DisplayName("序號")]
		public int Id { get; set; }

		/// <summary>
		/// 顏色編號
		/// </summary>
		[DisplayName("編號")]
		public int Seq { get; set; }

		/// <summary>
		/// 顏色名
		/// </summary>
		[DisplayName("顏色")]
		public string Name { get; set; }
	}
}
