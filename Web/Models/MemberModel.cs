using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	public class Member
	{
		/// <summary>
		/// 顧客編號
		/// </summary>
		[DisplayName("編號")]
		public int Id { get; set; }

		/// <summary>
		/// 會員姓名
		/// </summary>
		[DisplayName("姓名")]
		public string Name { get; set; }

		/// <summary>
		/// 餘額
		/// </summary>
		[DisplayName("餘額")]
		public int Amount { get; set; }

		/// <summary>
		/// 顧客電話
		/// </summary>
		[DisplayName("電話")] 
		public string Phone { get; set; }

		/// <summary>
		/// 會員地址
		/// </summary>
		[DisplayName("地址")] 
		public string Address { get; set; }

		/// <summary>
		/// 會員統編
		/// </summary>
		[DisplayName("統編")] 
		public string UniformNo { get; set; }

		/// <summary>
		/// 備註
		/// </summary>
		[DisplayName("備註")] 
		public string Remark { get; set; }

		/// <summary>
		/// 顧客 LineId
		/// </summary>
		[DisplayName("LineId")] 
		public string LineId  { get; set; }

		/// <summary>
		/// 建立時間
		/// </summary>
		[DisplayName("建立時間")]
		[DataType(DataType.DateTime)]
		[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")] 
		public DateTime CreateDt { get; set; }

		/// <summary>
		/// 建立者
		/// </summary>
		[DisplayName("建立者")] 
		public string CreateBy { get; set; }

		/// <summary>
		/// 建立時間
		/// </summary>
		[DisplayName("更新時間")]
		[DataType(DataType.DateTime)]
		[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")] 
		public DateTime? UpdateDt { get; set; }

		/// <summary>
		/// 建立者
		/// </summary>
		[DisplayName("更新者")]
		public string UpdateBy { get; set; }

		/// <summary>
		/// 衣服資訊
		/// </summary>
		public ICollection<Clothing> Enrollments { get; set; }
	}

	public class SearchMemberViewModel 
	{
		/// <summary>
		/// 顧客編號
		/// </summary>
		[DisplayName("顧客編號")]
		public int? MemberId { get; set; }

		/// <summary>
		/// 顧客姓名
		/// </summary>
		[DisplayName("顧客姓名")]
		public string Name { get; set; }

		/// <summary>
		/// 顧客電話
		/// </summary>
		[DisplayName("顧客電話")]
		public string Phone { get; set; }
	}
}
