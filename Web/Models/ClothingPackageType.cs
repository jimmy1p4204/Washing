using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	/// <summary>
	/// 衣物包裝方式
	/// </summary>
	public class ClothingPackageType
	{
		/// <summary>
		/// DB 序號
		/// </summary>
		[DisplayName("序號")]
		public int Id { get; set; }

		/// <summary>
		/// 包裝方式
		/// </summary>
		[DisplayName("包裝方式 ")]
		public string Name { get; set; }
	}
}
