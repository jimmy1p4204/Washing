using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
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
		/// 衣物編號 800 為特別編號，為自助洗，此類衣物需獨立統計金額
		/// (自助洗為本店自行收送整桶衣物，本店親洗，水洗，不會再送到外面乾洗、水洗。)
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

	public static class ClothingTyoeConst
	{
		/// <summary>
		/// 衣物編號(Id) 96 為特別編號 (對應 ClothingType 的 Seq = 800)，為自助洗，此類衣物需獨立統計金額
		/// (自助洗為本店自行收送整桶衣物，本店親洗，水洗，不會再送到外面乾洗、水洗。)
		/// </summary>
		public const int SelfWash = 96;
	}
}
