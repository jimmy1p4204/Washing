using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;

namespace Web.Controllers
{
    [Authorize(Roles = "SystemManager")]
    public class IdentityUserRolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IdentityUserRolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicationUsers
        public async Task<IActionResult> Index()
        {
            ViewBag.Users = _context.Users.ToDictionary(x=> x.Id, x=>x.UserName);
            ViewBag.Roles = _context.Roles.ToDictionary(x=>x.Id, x=>x.Name);
            return View(await _context.UserRoles.ToListAsync());
        }

        // GET: ApplicationUsers/Create
        public IActionResult Create()
        {
            SetUserSelectList();
            SetRoleSelectList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityUserRole<string> userRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userRole);
        }

        // GET: ApplicationUsers/Delete/5
        public async Task<IActionResult> Delete(IdentityUserRole<string> userRole)
        {
            if (userRole == null)
            {
                return NotFound();
            }

            var dbUserRole = await _context.UserRoles
                .FirstOrDefaultAsync(m => m.UserId == userRole.UserId && m.RoleId == userRole.RoleId);
            if (dbUserRole == null)
            {
                return NotFound();
            }

            _context.UserRoles.Remove(dbUserRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void SetUserSelectList()
        {
            var selectList = _context.Users.Select(x => new SelectListItem { Text = x.UserName, Value = x.Id.ToString() });
            ViewBag.Users = selectList;
        }

        private void SetRoleSelectList()
        {
            var selectList = _context.Roles.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            ViewBag.Roles = selectList;
        }

    }
}
