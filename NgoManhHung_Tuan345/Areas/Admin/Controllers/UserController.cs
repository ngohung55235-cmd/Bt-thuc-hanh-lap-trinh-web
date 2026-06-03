using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NgoManhHung_Tuan345.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NgoManhHung_Tuan345.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // List all users
        public async Task<IActionResult> Index()
        {
            var usersList = await _userManager.Users.ToListAsync();
            var userRolesVM = new List<UserManagementViewModel>();

            foreach (var user in usersList)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRolesVM.Add(new UserManagementViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    Address = user.Address,
                    Age = user.Age,
                    Roles = string.Join(", ", roles),
                    IsLocked = user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.UtcNow
                });
            }

            return View(userRolesVM);
        }

        // GET: Edit user role
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();

            var model = new EditRoleViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                CurrentRole = currentRoles.FirstOrDefault(),
                RoleList = allRoles.Select(r => new SelectListItem
                {
                    Text = r,
                    Value = r,
                    Selected = currentRoles.Contains(r)
                })
            };

            return View(model);
        }

        // POST: Edit user role
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            
            // Remove from current roles
            if (currentRoles.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    ModelState.AddModelError("", "Không thể xóa các vai trò hiện tại.");
                    return View(model);
                }
            }

            // Add to new selected role
            if (!string.IsNullOrEmpty(model.NewRole))
            {
                var addResult = await _userManager.AddToRoleAsync(user, model.NewRole);
                if (!addResult.Succeeded)
                {
                    ModelState.AddModelError("", "Không thể thêm vai trò mới.");
                    return View(model);
                }
            }

            TempData["Success"] = $"Đã cập nhật vai trò cho người dùng '{user.FullName}' thành công!";
            return RedirectToAction(nameof(Index));
        }

        // POST: Lock or Unlock user account
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LockUnlock(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Prevent self-lockout
            var currentUserId = _userManager.GetUserId(User);
            if (user.Id == currentUserId)
            {
                TempData["Error"] = "Bạn không thể tự khóa tài khoản của chính mình!";
                return RedirectToAction(nameof(Index));
            }

            if (user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.UtcNow)
            {
                // User is currently locked, unlock them
                user.LockoutEnd = null;
                TempData["Success"] = $"Đã mở khóa tài khoản '{user.FullName}' thành công!";
            }
            else
            {
                // User is unlocked, lock them for 100 years
                user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100);
                TempData["Success"] = $"Đã khóa tài khoản '{user.FullName}' thành công!";
            }

            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }

        // POST: Delete user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var currentUserId = _userManager.GetUserId(User);
            if (user.Id == currentUserId)
            {
                TempData["Error"] = "Bạn không thể xóa tài khoản của chính mình!";
                return RedirectToAction(nameof(Index));
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["Success"] = $"Đã xóa tài khoản '{user.FullName}' khỏi hệ thống!";
            }
            else
            {
                TempData["Error"] = "Đã xảy ra lỗi khi xóa tài khoản.";
            }

            return RedirectToAction(nameof(Index));
        }
    }

    public class UserManagementViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Age { get; set; }
        public string Roles { get; set; }
        public bool IsLocked { get; set; }
    }

    public class EditRoleViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string CurrentRole { get; set; }
        public string NewRole { get; set; }
        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}
