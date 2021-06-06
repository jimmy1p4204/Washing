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
	public class ClothingAction
	{
		/// <summary>
		/// DB 序號
		/// </summary>
		[DisplayName("序號")]
		public int Id { get; set; }

		/// <summary>
		/// 清洗方式(乾洗或水洗)
		/// </summary>
		[DisplayName("清洗方式")]
		public string Name { get; set; }
	}
}
