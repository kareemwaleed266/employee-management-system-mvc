using Demo.DAL.Entities;
using Demo.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
    [Authorize(Roles = "Admin")]

    public class RolesController : Controller
    {
        private readonly ILogger<RolesController> _logger;

        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly UserManager<ApplicationUser> _userManager;
        public RolesController(RoleManager<ApplicationRole>roleManager,ILogger<RolesController> logger,UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult >Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        } 
        public IActionResult Create()
        {
            return View(new ApplicationRole());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationRole role)
        {
            if(ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    TempData["MessageTemp"] = "Role Added Successfully";
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    _logger.LogError(error.Description);
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(role);
        }
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
            {
                return NotFound();
            }
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }
            return View(viewName, role);
        }

        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, ApplicationRole appRole)
        {
            if (id != appRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(id);
                role.Name = appRole.Name;
                role.NormalizedName = appRole.Name.ToUpper();

                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    TempData["MessageTempUpdated"] = "role Updated Successfully";

                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    _logger.LogError(error.Description);
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(appRole);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                TempData["MessageTempDeleted"] = "role Deleted Successfully";

                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
            {
                _logger.LogError(error.Description);
                ModelState.AddModelError("", error.Description);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            ViewBag.RoleId = roleId;

            if(role is null) 
                return NotFound();


            var usersInRole= new List<UserInRoleViewModel>();
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var userInRole = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UsernName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                    userInRole.IsSelected= false;

                usersInRole.Add(userInRole);
            }
            return View(usersInRole);

        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId,List<UserInRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if(role is null) 
                return NotFound();
            if(ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if (appUser is null)
                        return NotFound();
                    else
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appUser, role.Name))
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name)) 
                        await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                    }
                }

                //foreach (var user in users)
                //{
                //    var appUser = await _userManager.FindByIdAsync(user.UserId);
                //    if(appUser != null)
                //    {
                //        if (user.IsSelected && !await _userManager.IsInRoleAsync(appUser, role.Name)) 
                //            await _userManager.AddToRoleAsync(appUser, role.Name);
                //        if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name)) 
                //            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                //    }
                //} 
                return RedirectToAction("Update",  new{ id = roleId });
            }
             return View(users);
        }
    }
}
