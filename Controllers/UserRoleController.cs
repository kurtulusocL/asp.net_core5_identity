using IdentityCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRoleController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRoleController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index(string userId)
        {
            var viewModel = new List<UserRoleViewModel>();
            var user = await _userManager.FindByIdAsync(userId);
            foreach (var item in _roleManager.Roles)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    RoleName = item.Name
                };
                if (await _userManager.IsInRoleAsync(user, item.Name))
                {
                    userRoleViewModel.Selected = true;
                }
                else
                {
                    userRoleViewModel.Selected = false;
                }
                viewModel.Add(userRoleViewModel);
            }
            var model = new ManageUserRolesViewModel()
            {
                UserId = userId,
                UserRoles = viewModel
            };
            return View(model);
        }
        public async Task<IActionResult> Update(string id, ManageUserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            result = await _userManager.AddToRolesAsync(user, model.UserRoles.Where(i=>i.Selected).Select(i=>i.RoleName));
            var currentUser = await _userManager.GetUserAsync(User);
            await _signInManager.RefreshSignInAsync(currentUser);
            await Seeds.DefaultUsers.SeedAdminAsync(_userManager, _roleManager);

            return RedirectToAction("Index", new { UserId = id });
        }
    }
}
