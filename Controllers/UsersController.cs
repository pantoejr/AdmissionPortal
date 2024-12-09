using AdmissionPortal.Data;
using AdmissionPortal.Models;
using AdmissionPortal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace AdmissionPortal.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        public UsersController(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["GroupID"] = new SelectList(_context.Groups, "Id", "Name");
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Error creating user";
                TempData["Flag"] = "red";
                ViewData["GroupID"] = new SelectList(_context.Groups, "Id", "Name", model.GroupID);
            }
            var appUser = new AppUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                LoginHint = model.ConfirmPassword,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                IsActive = true,
            };
            var groupRoles = await _context.GroupRoles.Include(x => x.Role).Select(x => x.Role.Name).ToListAsync();
            var result = await _userManager.CreateAsync(appUser, model.ConfirmPassword);
            if (result.Succeeded)
            {
                var userGroup = new GroupUser
                {
                    GroupID = model.GroupID,
                    AppUserID = appUser.Id,
                    IsActive = true,
                };
                await _context.GroupUsers.AddAsync(userGroup);
                await _userManager.AddToRolesAsync(appUser, groupRoles);
                await _context.SaveChangesAsync();
                TempData["Message"] = "User created successfully";
                TempData["Flag"] = "green";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(String Id)
        {
            var existingUser = await _userManager.FindByIdAsync(Id);

            var group = await _context.GroupUsers.Include(x => x.Group)
                .Where(x => x.AppUserID.Equals(existingUser.Id))
                .Select(x => x.Group)
                .FirstOrDefaultAsync();

            ViewData["GroupID"] = new SelectList(_context.Groups, "Id", "Name", group.Id);

            var user = new UserViewModel
            {
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                Email = existingUser.Email,
                PhoneNumber = existingUser.PhoneNumber,
                Password = existingUser.LoginHint,
                GroupID = group.Id
            };
            return View(user);
        }

        public async Task<IActionResult> Details(string Id)
        {
            var existingUser = await _userManager.FindByIdAsync(Id);

            var group = await _context.GroupUsers.Include(x => x.Group)
                .Where(x => x.AppUserID.Equals(existingUser.Id))
                .Select(x => x.Group)
                .FirstOrDefaultAsync();

            ViewData["GroupID"] = new SelectList(_context.Groups, "Id", "Name", group.Id);

            var user = new UserViewModel
            {
                UserID = existingUser.Id,
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                Email = existingUser.Email,
                PhoneNumber = existingUser.PhoneNumber,
                Password = existingUser.LoginHint,
                GroupID = group.Id,
                LoginHint = existingUser.LoginHint,
            };
            return View(user);
        }

        public async Task<IActionResult> RefreshRoles(int GroupID, string UserID)
        {
            if (GroupID != null && UserID != null)
            {
                var result = await _context.UpdateUserRoles(GroupID, UserID);
                if (result == 100)
                {
                    TempData["Message"] = "Roles refreshed successfully";
                    TempData["Flag"] = "green";
                    return RedirectToAction(nameof(Details), new { Id = UserID });
                }
            }
            TempData["Message"] = "Error refreshing user roles";
            TempData["Flag"] = "red";
            return RedirectToAction(nameof(Details), new { Id = UserID });
        }

        [HttpGet]
        public async Task<IActionResult> UnAvUserRoles(string Id)
        {

            var unAvailableUserRoles = await _context.GetUserRolesById(new SqlParameter("@UserID", Id));
            ViewData["UserID"] = Id;
            return PartialView("_UnAvUserRoles", unAvailableUserRoles);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            await _userManager.DeleteAsync(user);
            TempData["Message"] = "User deleted successfully";
            TempData["Flag"] = "red";
            return RedirectToAction(nameof(Index));
        }
    }
}
