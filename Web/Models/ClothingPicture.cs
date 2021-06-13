using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	/// <summary>
	/// 衣服照片資料
	/// </summary>
	public class ClothingPicture
	{
		/// <summary>
		/// 衣物編號
		/// </summary>
		[DisplayName("編號")]
		public int Id { get; set; }

		/// <summary>
		/// 衣物Id
		/// </summary>
		[DisplayName("衣物編號")]
		public int ClothingId { get; set; }

		/// <summary>
		/// 照片內容 (Base64 字串)
		/// </summary>
		[DisplayName("照片內容")]
		public string Content { get; set; }

		/// <summary>
		/// 拍照日期
		/// </summary>
		[DisplayName("拍照日期")]
		[DataType(DataType.DateTime)]
		[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
		public DateTime CreateDt { get; set; } = DateTime.Now;

	}
}
