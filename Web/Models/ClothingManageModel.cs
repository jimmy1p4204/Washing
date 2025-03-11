using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;

namespace Web.Models
{
	/// <summary>
	/// 未清洗衣物
	/// </summary>
	public class ClothingManageViewModel
	{
		public int Id { get; set; }

		/// <summary>
		/// 衣物編號
		/// </summary>
		[DisplayName("衣物編號")] 
		public int? ClothingSeq { get; set; }

		/// <summary>
		/// 會員編號
		/// </summary>
		[DisplayName("會員編號")]

		public int MemberId { get; set; }

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
		public ClothingStatusEnum Status { get; set; }

		/// <summary>
		/// 付款狀態
		/// </summary>
		[DisplayName("付款狀態")]
		public bool Paid { get; set; }

		/// <summary>
		/// 取件日期
		/// </summary>
		[DisplayName("取件日期")]
		public DateTime? PickupDt { get; internal set; }

		/// <summary>
		/// 類型
		/// </summary>
		[DisplayName("類型")]
		public int Type { get; internal set; }

		/// <summary>
		/// 顏色
		/// </summary>
		[DisplayName("顏色")]
		public string Color { get; internal set; }

		/// <summary>
		/// 顏色
		/// </summary>
		[DisplayName("顏色")]
		public string ColorStr { get; internal set; }

		/// <summary>
		/// 顏色
		/// </summary>
		[DisplayName("金額")]
		public int Amount { get; internal set; }
	}

	public static class ClothingManageViewModelExtentions
	{
		public static List<ClothingManageViewModel> GetColorMapping(this List<ClothingManageViewModel> model, WashingDbContext context)
		{
			// 將顏色編號轉成顏色
			var colors = context.ClothingColors.ToDictionary(x => x.Id, x => x.Name);
			model.ForEach(x =>
			{
				var colorIds = x.Color.Split(',');
				x.ColorStr = string.Join(",", colorIds.Select(y => colors[int.Parse(y)]));
			});

			return model;
		}
	}
}
