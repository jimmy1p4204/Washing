using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	public class ClothingType
	{
		/// <summary>
		/// DB 序號
		/// </summary>
		[DisplayName("序號")]
		public int Id { get; set; }

		/// <summary>
		/// 衣物編號
		/// </summary>
		[DisplayName("編號")]
		public int Seq { get; set; }

		/// <summary>
		/// 衣物種類名稱
		/// </summary>
		[DisplayName("名稱")]
		public string Name { get; set; }

		/// <summary>
		/// 衣物規格
		/// </summary>
		[DisplayName("衣物規格")]
		public string Spec { get; set; }

		/// <summary>
		/// 水洗價格
		/// </summary>
		[DisplayName("水洗")]
		public int WashingPrice { get; set; }

		/// <summary>
		/// 乾洗價格
		/// </summary>
		[DisplayName("乾洗")]
		public int DryCleaningPrice { get; set; }

		/// <summary>
		/// 備註
		/// </summary>
		[DisplayName("備註")]
		public string Remark { get; set; }
	}
}
