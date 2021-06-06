using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
	/// <summary>
	/// WID 檔 - 可能是顧客洗衣資料
	/// </summary>
	public class Wid
	{
		/// <summary>
		/// 衣物編號
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Column 1 (尚不知道欄位作用)
		/// </summary>
		public string C1 { get; set; }

		/// <summary>
		/// Column 2 (尚不知道欄位作用)
		/// </summary>
		public string C2 { get; set; }

		/// <summary>
		/// 會員編號=
		/// </summary>
		public string MemberNo { get; set; }

		/// <summary>
		/// 會員姓名
		/// </summary>
		public string MemberName { get; set; }

		/// <summary>
		/// 衣物編號
		/// </summary>
		public string CommodityNo { get; set; }

		/// <summary>
		/// 衣物名稱
		/// </summary>
		public string CommodityName { get; set; }

		/// <summary>
		/// 衣物色調(逗號分隔)
		/// </summary>
		public string Color { get; set; }

		/// <summary>
		/// 處理方式編號
		/// </summary>
		public string MethodNo { get; set; }

		/// <summary>
		/// 處理方式描述
		/// </summary>
		public string MethodName { get; set; }

		/// <summary>
		/// 備註
		/// </summary>
		public string Remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string C3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string C4 { get; set; }

		/// <summary>
		/// 時間1(字串格式為 yyyyMMddHH:mm:ss
		/// </summary>
		public string DateTime { get; set; }

		/// <summary>
		/// 時間2(字串格式為 yyyyMMdd
		/// </summary>
		public string DateTime2 { get; set; }

		/// <summary>
		/// 時間3(字串格式為 yyyyMMddHH:mm:ss
		/// </summary>
		public string DateTime3 { get; set; }

		/// <summary>
		/// 時間4(字串格式為 yyyyMMddHH:mm:ss
		/// </summary>
		public string DateTime4 { get; set; }

		/// <summary>
		/// (可能是員工編號)
		/// </summary>
		public string C5 { get; set; }

		/// <summary>
		/// 員工姓名
		/// </summary>
		public string StaffName { get; set; }

		/// <summary>
		/// (也可能是員工編號)
		/// </summary>
		public string C6 { get; set; }

		/// <summary>
		/// 員工姓名2
		/// </summary>
		public string StaffName2 { get; set; }

		/// <summary>
		/// (也可能是員工編號)
		/// </summary>
		public string C7 { get; set; }

		/// <summary>
		/// 員工姓名3
		/// </summary>
		public string StaffName3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string C8 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string C9 { get; set; }
	}
}
