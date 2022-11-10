using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Manager,Employee")]
    public class LogsController : Controller
    {
        private readonly WashingDbContext _context;

        public LogsController(WashingDbContext context)
        {
            _context = context;

           
        }

        public async Task<IActionResult> Member(int? memberId)
        {
            IQueryable<Log> logs = _context.Logs;

            if (memberId == null || memberId == 0)
			{
                return NotFound();
			}

            logs = logs.Where(x => x.MemberId == memberId);

            var logList = await logs.OrderByDescending(x => x.LogDt).ToListAsync();

            GetViewBag();

            ViewData["Title"] = "系統紀錄";
            return View("Index", logList);
        }

        /// <summary>
        /// 本日儲值金額
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> TodayDeposit() 
        {
            IQueryable<Log> logs = _context.Logs.Where(x=>x.LogDt > DateTime.Today && x.Act == LogAct.儲值);

            var logList = await logs.OrderByDescending(x => x.LogDt).ToListAsync();

            GetViewBag();

            ViewData["Title"] = "本日儲值紀錄";
            return View("Index", logList);
        }

        /// <summary>
		/// 本日儲值金額
		/// </summary>
		/// <returns></returns>
		[Authorize(Roles = "Manager")]
        public async Task<IActionResult> ThisMonthDeposit()
        {
            IQueryable<Log> logs = _context.Logs.Where(x => x.LogDt.Year == DateTime.Today.Year && x.LogDt.Month == DateTime.Today.Month && x.Act == LogAct.儲值);

            var logList = await logs.OrderByDescending(x => x.LogDt).ToListAsync();

            GetViewBag();

            ViewData["Title"] = "本月儲值紀錄";
            return View("Index", logList);
        }

        /// <summary>
		/// 本日操作紀錄
		/// </summary>
		/// <returns></returns>
        public async Task<IActionResult> Today()
        {
            IQueryable<Log> logs = _context.Logs.Where(x => x.LogDt > DateTime.Today);

            var logList = await logs.OrderByDescending(x => x.LogDt).ToListAsync();

            GetViewBag();

            ViewData["Title"] = "本日操作紀錄";
            return View("Index", logList);
        }

        /// <summary>
		/// 本月操作紀錄
		/// </summary>
		/// <returns></returns>
		[Authorize(Roles = "Manager")]
        public async Task<IActionResult> Month()
        {
            IQueryable<Log> logs = _context.Logs.Where(x => x.LogDt.Year == DateTime.Today.Year && x.LogDt.Month == DateTime.Today.Month);

            var logList = await logs.OrderByDescending(x => x.LogDt).ToListAsync();

            GetViewBag();

            ViewData["Title"] = "本月操作紀錄";
            return View("Index", logList);
        }

        /// <summary>
        /// 指定月份資料 (取三個月)
        /// </summary>
        /// <param name="offset">偏移量(月)</param>
        /// <returns></returns>
		[Authorize(Roles = "Manager, SystemManager")]
        public async Task<IActionResult> Offset(int offset)
        {
            var startDt = DateTime.Today.AddMonths(-offset);
            var endDt = startDt.AddMonths(3);
            IQueryable<Log> logs = _context.Logs.Where(x => x.LogDt >= startDt && x.LogDt <= endDt);

            var logList = await logs.OrderByDescending(x => x.LogDt).ToListAsync();

            GetViewBag();

            ViewData["Title"] = $"{startDt.ToShortDateString()} ~ {endDt.ToShortDateString()} 操作紀錄";
            return View("Index", logList);
        }

        /// <summary>
        /// 取得畫面呈現所需資訊(ViewBag)
        /// </summary>
        private void GetViewBag()
        {
            // 會員 (呈現中文用)
            ViewBag.Members = _context.Members.ToDictionary(x => x.Id, x => x);

            // 衣物類型對應 (呈現中文用)
            ViewBag.Clothings = _context.Clothings.ToDictionary(x => x.Id, x => x);

            // 衣物類型對應 (呈現中文用)
            ViewBag.ClothingTypes = _context.ClothingTypes.ToDictionary(x => x.Seq, x => x.Name);
        }
    }
}
