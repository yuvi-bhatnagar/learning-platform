using learning_platform.Areas.Admin.Models;
using learning_platform.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace learning_platform.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Admins()
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            var superAdmins = await _userManager.GetUsersInRoleAsync("SuperAdmin");
            
            // Combine lists and de-duplicate by ID
            var allAdmins = admins.Concat(superAdmins).DistinctBy(u => u.Id).ToList();

            var adminList = new List<AdminUserRow>();
            foreach (var user in allAdmins)
            {
                var roles = await _userManager.GetRolesAsync(user);
                adminList.Add(new AdminUserRow
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty,
                    Role = string.Join(", ", roles)
                });
            }

            return View(adminList);
        }

        [HttpGet]
        public IActionResult CreateAdmin()
        {
            return View(new CreateAdminViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin(CreateAdminViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Guard: Assert that target role is valid
            if (model.Role != "Admin" && model.Role != "SuperAdmin")
            {
                ModelState.AddModelError("Role", "Invalid role selection.");
                return View(model);
            }

            // Check if user already exists
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email address is already registered.");
                return View(model);
            }

            var newUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(newUser, model.Password);
            if (createResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, model.Role);
                return RedirectToAction(nameof(Admins));
            }

            foreach (var error in createResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditAdmin(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Admin") && !roles.Contains("SuperAdmin"))
            {
                return RedirectToAction(nameof(Admins));
            }

            var model = new EditAdminViewModel
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                Role = roles.Contains("SuperAdmin") ? "SuperAdmin" : "Admin"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdmin(EditAdminViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            // Check if email changed and is already taken
            if (user.Email != model.Email)
            {
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email address is already registered.");
                    return View(model);
                }
                
                user.Email = model.Email;
                user.UserName = model.Email;
            }

            // Update role if changed
            var currentRoles = await _userManager.GetRolesAsync(user);
            var targetRole = model.Role;

            if (targetRole != "Admin" && targetRole != "SuperAdmin")
            {
                ModelState.AddModelError("Role", "Invalid role selection.");
                return View(model);
            }

            if (!currentRoles.Contains(targetRole))
            {
                // Lockout prevention: SuperAdmin cannot remove their own SuperAdmin role if they are the current user
                var currentUserId = _userManager.GetUserId(User);
                if (user.Id == currentUserId && targetRole == "Admin" && currentRoles.Contains("SuperAdmin"))
                {
                    ModelState.AddModelError("Role", "You cannot demote yourself from SuperAdmin.");
                    return View(model);
                }

                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, targetRole);
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                // Update password if provided
                if (!string.IsNullOrEmpty(model.Password))
                {
                    var hasPassword = await _userManager.HasPasswordAsync(user);
                    if (hasPassword)
                    {
                        var removePassResult = await _userManager.RemovePasswordAsync(user);
                        if (!removePassResult.Succeeded)
                        {
                            foreach (var error in removePassResult.Errors)
                            {
                                ModelState.AddModelError("Password", error.Description);
                            }
                            return View(model);
                        }
                    }

                    var addPassResult = await _userManager.AddPasswordAsync(user, model.Password);
                    if (!addPassResult.Succeeded)
                    {
                        foreach (var error in addPassResult.Errors)
                        {
                            ModelState.AddModelError("Password", error.Description);
                        }
                        return View(model);
                    }
                }

                return RedirectToAction(nameof(Admins));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAdmin(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var currentUserId = _userManager.GetUserId(User);
            if (id == currentUserId)
            {
                TempData["ErrorMessage"] = "You cannot delete your own account.";
                return RedirectToAction(nameof(Admins));
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Admin") && !roles.Contains("SuperAdmin"))
            {
                return RedirectToAction(nameof(Admins));
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = "Failed to delete user account.";
            }

            return RedirectToAction(nameof(Admins));
        }
    }
}
