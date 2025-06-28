using Demo.DAL.Entities;
using Demo.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
        [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountController> _logger;

        public UserController(UserManager<ApplicationUser> userManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<IActionResult> Index(string SearchValue = "")
        {

            List<ApplicationUser> users;

            if (string.IsNullOrEmpty(SearchValue))
            {
                users = await _userManager.Users.ToListAsync();
            }

            else
            {
                users = await _userManager.Users
                    .Where(user => user.Email.Trim().ToLower().Contains(SearchValue.Trim().ToLower())).ToListAsync();
            }

            return View(users);
        }

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            return View(viewName, user);
        }

        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, ApplicationUser appUser)
        {
            if (id != appUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                user.UserName = appUser.UserName;
                user.NormalizedUserName = appUser.UserName.ToUpper();

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["MessageTempUpdated"] = "User Updated Successfully";

                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    _logger.LogError(error.Description);
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(appUser);
        }

        public async Task<IActionResult> Delete(string? id)
        { 
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["MessageTempDeleted"] = "User Deleted Successfully";

                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
            {
                _logger.LogError(error.Description);
                ModelState.AddModelError("", error.Description);
            }
            return RedirectToAction("Index");
        }
    }
}
