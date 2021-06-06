using FileHelpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
	public class TestController : Controller
	{
		private readonly ILogger<TestController> _logger;
		private readonly WashingDbContext _context;
		private readonly IHostingEnvironment _hostingEnvironment;

		public TestController(ILogger<TestController> logger, WashingDbContext context, IHostingEnvironment hostingEnvironment)
		{
			_logger = logger;
			_context = context;
			_hostingEnvironment = hostingEnvironment;
		}

		public IActionResult Wid()
		{
			var path = Path.Combine((string)AppDomain.CurrentDomain.BaseDirectory, "App_Data/Washing/Wid.bdf_ThirdPart.txt");

			var content = System.IO.File.ReadAllText(path);
			var strArray = content.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			var strArrayLen = strArray.Length;
			var models = new List<Wid>();
			int i = 0; // index
			while (i + 16 < strArrayLen)
			{
				var model = new Wid();
				model.Id = strArray[i++];
				model.C1 = strArray[i++];
				model.C2 = strArray[i++];

				var substr = strArray[i++];
				string pattern = @"^(?<Number>\d+)\s*(?<Name>[^\s]+)$";
				Regex r = new Regex(pattern, RegexOptions.Compiled);
				Match m = r.Match(substr);
				model.MemberNo = m.Groups["Number"].Value;
				model.MemberName = m.Groups["Name"].Value;

				substr = strArray[i++];
				m = r.Match(substr);
				model.CommodityNo = m.Groups["Number"].Value;
				model.CommodityName = m.Groups["Name"].Value;


				model.Color = strArray[i++];

				if (Char.IsDigit(model.Color[0])) 
				{
					model.Color = "無";
					i--;
				}

				substr = strArray[i++];
				pattern = @"^(?<Number>\d+)\s*(?<NameAndRemark>[^\s]+)$";
				r = new Regex(pattern, RegexOptions.Compiled);
				m = r.Match(substr);
				model.MethodNo = m.Groups["Number"].Value;
				model.MethodName = m.Groups["NameAndRemark"].Value.Substring(0,2);
				model.Remark = m.Groups["NameAndRemark"].Value.Substring(2);

				model.C3 = strArray[i++];

				int dt_length = 16 + 8 + 16 + 16;
				substr = strArray[i++];
				model.C4 = substr.Substring(0, substr.Length - dt_length);
				model.DateTime = substr.Substring(model.C4.Length, 16);
				model.DateTime2 = substr.Substring(model.C4.Length + 16, 8);
				model.DateTime3 = substr.Substring(model.C4.Length + 16 + 8, 16);
				model.DateTime4 = substr.Substring(model.C4.Length + 16 + 8 + 16, 16);
				
				model.C5 = strArray[i++];
				model.StaffName = strArray[i++];
				model.C6 = strArray[i++];
				model.StaffName2 = strArray[i++];
				model.C7 = strArray[i++];
				model.StaffName3 = strArray[i++];
				model.C8 = strArray[i++];
				model.C9 = strArray[i++];
				;
				//l = 12; model.ID = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 3; model.C1 = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 3; model.C2 = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 6; model.MemberNo = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 20; model.MemberName = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 6; model.CommodityNo = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 38; model.CommodityName = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 28; model.Color = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 4; model.MethodNo = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 6; model.MethodName = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 103; model.Remark = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 8; model.C3 = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 8; model.C4 = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 16; model.DateTime = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 8; model.DateTime2 = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 16; model.DateTime3 = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 16; model.DateTime4 = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 11; model.C5 = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 23; model.StaffName = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 6; model.C6 = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 23; model.StaffName2 = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 6; model.C7 = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 23; model.StaffName3 = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 16; model.C8 = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				//l = 35; model.C9 = System.Text.Encoding.GetEncoding("big5").GetString(bytes.Skip(i).Take(l).ToArray()); i += l;
				models.Add(model);
			}
			

			//var engine = new FileHelperEngine<Wid>();

			// To Read Use: 
			//var res = engine.ReadFile(path);

			return View(models);
		}

		public IActionResult Cst()
		{
			//var path = Path.Combine((string)AppDomain.CurrentDomain.BaseDirectory, "App_Data/Washing/CST.dbf_two.txt");
			var path = Path.Combine((string)AppDomain.CurrentDomain.BaseDirectory, "App_Data/Washing/CST.dbf_all_from_78.txt");

			var content = System.IO.File.ReadAllText(path);
			content = content.Replace("\r\n", "");
			var strArray = content.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			var strArrayLen = strArray.Length;
			var models = new List<Cst>();
			int i = 0; // index
			while (i + 25 < strArrayLen)
			{
				var model = new Cst();

				var substr = strArray[i++];
				string pattern = @"^(?<Number>\d+)\s*(?<Name>[^\s]+)$";
				Regex r = new Regex(pattern, RegexOptions.Compiled);
				Match m = r.Match(substr);
				model.Id = m.Groups["Number"].Value;
				model.Name = m.Groups["Name"].Value;

				substr = strArray[i++];
				if (substr.Length == 9 && substr.All(char.IsDigit)) 
				{
					model.UniformNo = substr.Substring(1);
					substr = strArray[i++];
				}

				model.C1 = substr.Substring(0,1);
				model.Address = substr.Substring(1);

				model.C2 = strArray[i++];
				model.C3 = strArray[i++];

				substr = strArray[i++];
				model.C4 = substr.Substring(0, 2);
				model.Date1 = substr.Substring(2, 8);
				model.DateTime1 = substr.Substring(10);

				model.C5 = strArray[i++];

				model.EmployeeId = strArray[i++];
				model.EmployeeName = strArray[i++];

				model.C6 = strArray[i++];
				model.ShopName = strArray[i++];

				model.C7 = strArray[i++];

				model.PhoneName = strArray[i++];

				substr = strArray[i++];
				while (substr != "11" && substr != "21") 
				{
					model.Remark += substr + "，";
					substr = strArray[i++];
				}
				model.C8 = substr;
				

				model.C9 = strArray[i++];
				model.C10 = strArray[i++];

				substr = strArray[i++];
				if (substr == "1" )
				{
					model.C11 = substr;
				}
				else
				{
					model.C11 = substr.Substring(0,1);
					model.C12 = substr.Substring(1);
				}

				model.C13 = strArray[i++];
				substr = strArray[i++];
				model.C14 = substr.Substring(0, substr.Length - 2);
				model.C15 = substr.Substring(substr.Length - 2);

				model.C16 = strArray[i++];

				substr = strArray[i++];
				if (substr.Substring(0, 2) == "01") {
					model.C18 = substr;
				}
				else if (substr.Substring(0, 2) != "01")
				{
					model.C17 = substr.Substring(0, 2);
					model.C18 = substr.Substring(2, 2);
					if (substr.Length >= 20) 
					{ 
						model.DateTime2 = substr.Substring(4, 16); 
					}
				}
				else
				{
					model.C18 = substr.Substring(0, 2);
					model.DateTime2 = substr.Substring(2, 16);
				}

				substr = strArray[i++];
				if (substr == "1") 
				{
					;
				}
				else 
				{
					model.Date2 = substr.Substring(0, 8);
				}


				substr = strArray[i++];
				if (substr == "0") 
				{
					models.Add(model);
					continue;
				}

				model.C19 = substr.Substring(0, 1);
				model.Date3 = substr.Substring(1, 8);

				model.C20 = strArray[i++];

				substr = strArray[i++];
				if (!substr.All(char.IsDigit))
				{
					i--; // 回到上一個 index, 讓下一 round 可以抓到正確的資料
				}
				else 
				{
					model.C21 = substr;
				}

				models.Add(model);
			}


			//var engine = new FileHelperEngine<Wid>();

			// To Read Use: 
			//var res = engine.ReadFile(path);

			return View(models);
		}

	}
}
