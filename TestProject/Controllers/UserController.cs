using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;
using TestProject.ViewModels;

namespace TestProject.Controllers
{
    
    public class UsersController : Controller
    {
        UserManager<User> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext db;
        private readonly SignInManager<User> _signInManager;
        IWebHostEnvironment _appEnvironment;

        public UsersController(ILogger<HomeController> logger, ApplicationContext context, IWebHostEnvironment appEnvironment, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            db = context;
            _appEnvironment = appEnvironment;
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Index() {
            if (User.IsInRole("admin"))
            {
                ViewBag.subjects = db.Subjects.ToList();
                return View(_userManager.Users.ToList());
            }
            else
            {
                return StatusCode(403);
            }
        }

        public IActionResult Create() {
            if (User.IsInRole("admin"))
            {
                return View();
            }
            else
            {
                return StatusCode(403);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (User.IsInRole("admin"))
            {
                if (ModelState.IsValid)
                {
                    User user = new User { Email = model.UserName + "@gmail.com", UserName = model.UserName };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    await _userManager.AddToRoleAsync(user, "user");
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                return View(model);
            }
            else
            {
                return StatusCode(403);
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (User.IsInRole("admin"))
            {
                User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                EditUserViewModel model = new EditUserViewModel { Id = user.Id, UserName = user.UserName, UserId = user.Id,
                UserEmail = user.Email,
                UserRoles = userRoles,
                AllRoles = allRoles
            };
            return View(model);
            }
            else
            {
                return StatusCode(403);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (User.IsInRole("admin"))
            {
                if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.UserName + "@gmail.com";
                    user.UserName = model.UserName;
                        var userRoles = await _userManager.GetRolesAsync(user);
                        var allRoles = _roleManager.Roles.ToList();
                        var addedRoles = model.UserRoles.Except(userRoles);
                        var removedRoles = userRoles.Except(model.UserRoles);
                        await _userManager.AddToRolesAsync(user, addedRoles);
                        await _userManager.RemoveFromRolesAsync(user, removedRoles);
                        var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
            }
            else
            {
                return StatusCode(403);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }

        public IActionResult UserList() => View(_userManager.Users.ToList());

        
    }
}

