using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [Authorize(Roles = "SystemManager")]
    public class SystemManagementController : Controller
	{
        private readonly WashingDbContext _context;

		public SystemManagementController(WashingDbContext context)
		{
            _context = context;
        }

        /// <summary>
        /// 刪除已取件超過六個月的衣物的圖片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public async Task<IActionResult> DeletePictureOverSixMonthComfirm()
        {
            int month = 6; // 要刪除多久前的資料(月)

            var query = _context.ClothingPictures.FromSqlRaw($@"SELECT TOP 1 * FROM [ClothingPictures]
WHERE Id IN (
    SELECT TOP 20
        T2.Id
    FROM [dbo].[Clothing]  AS T1 
    JOIN [dbo].[ClothingPictures] AS T2 
    ON T1.Id = T2.ClothingId
    WHERE IsPickup = 1 AND PickupDt < DATEADD(MONTH, -{month}, GETDATE())
    ORDER BY T2.CreateDt
)
ORDER BY CreateDt");
            if(query.Count() > 0)
			{
                ViewBag.OldestDt = $"已取件之最舊照片時間為 {query.First().CreateDt}";
            }
			else
			{
                ViewBag.OldestDt = $"無超過{month}個月的資料";
            }
            
            return View();
        }

        /// <summary>
        /// 刪除已取件超過六個月的衣物的圖片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeletePictureOverSixMonth()
        {
            int month = 6; // 要刪除多久前的資料(月)
            //  (因 DB 效能問題，一次刪 20 好像是最多了)

            var task = await _context.Database.ExecuteSqlInterpolatedAsync($@"
DELETE [dbo].[ClothingPictures]
WHERE Id IN (
    SELECT TOP 20
        T2.Id
    FROM [dbo].[Clothing]  AS T1 
    JOIN [dbo].[ClothingPictures] AS T2 
    ON T1.Id = T2.ClothingId
    WHERE IsPickup = 1 AND PickupDt < DATEADD(MONTH, -{month}, GETDATE())
)");
            ViewBag.DeletePicCount = task;

            return View();
        }
    }
}
