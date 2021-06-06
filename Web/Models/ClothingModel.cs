﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	public class Clothing
	{
		/// <summary>
		/// 衣物編號
		/// </summary>
		[DisplayName("編號")]
		public int Id { get; set; }

		/// <summary>
		/// 衣物對應的會員編號
		/// </summary>
		[DisplayName("會員編號")]
		public int MemberId { get; set; }

		/// <summary>
		/// 衣服類型
		/// </summary>
		[DisplayName("類型")]
		public int Type { get; set; }

		/// <summary>
		/// 衣服顏色
		/// </summary>
		[DisplayName("顏色")]
		public int Color { get; set; }

		/// <summary>
		/// 清洗方式(水洗或乾洗)
		/// </summary>
		[DisplayName("清洗方式")]
		public int Action { get; set; }

		/// <summary>
		/// 衣服附註
		/// </summary>
		[DisplayName("附註")]
		public string Remark { get; set; }

		/// <summary>
		/// 衣服單價
		/// </summary>
		[DisplayName("單價")]
		public int Amount { get; set; }

		/// <summary>
		/// 折讓金額
		/// </summary>
		[DisplayName("折讓金額")]
		public int DiscountAmount { get; set; }

		/// <summary>
		/// 取件日期
		/// </summary>
		[DisplayName("取件日期")]
		public DateTime PickupDt { get; set; }

		/// <summary>
		/// 照片編號
		/// </summary>
		[DisplayName("照片編號")]
		public int PicNo { get; set; }
	}
}
