using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagement.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using EmployeeManagement.Model;

namespace EmployeeManagement.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<AdministrationController> logger;
        public AdministrationController(RoleManager<IdentityRole> roleManager
                                        , UserManager<IdentityUser> userManager
                                        , ILogger<AdministrationController> logger)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = userManager.Users;
            return View(users);
        }
        [HttpGet]
        public IActionResult ListRole()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole", "Administration");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Policy ="DeleteRolePolicy")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"The role Id {id} can not be found";
                return View("NotFound");
            }
            else
            {
                try
                {
                    var result = await roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRole");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View("ListRole");
                }
                catch (DbUpdateException ex)
                {
                    logger.LogError($"Error Deleting Role {ex}");
                    ViewBag.ErrorTitle = $"the '{role.Name}' is in used";
                    ViewBag.ErrorMessage = $"the '{role.Name}' can not be deleted as there are users " +
                        $"in this role, If you want to delete this role , please remove the users from " +
                        $"the role and try again";

                    return View("Error");
                }
            }

        }


        [HttpGet]
        [Authorize(Policy = "DeleteRolePolicy")]
        public async Task<IActionResult> EditRole(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id {Id} can not be found";
                return View("NotFound");
            }
            else
            {

                var model = new EditRoleViewModel()
                {
                    Id = role.Id,
                    RoleName = role.Name
                };

                foreach (var user in userManager.Users)
                {
                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        model.Users.Add(user.UserName);
                    };
                }
                return View(model);
            }

        }
        [HttpPost]
        [Authorize(Policy = "DeleteRolePolicy")]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id {model.Id} can not be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;


                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

        }


        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"The Role with Id {roleId} can not be found";
                return View("NotFound");
            }
            var usersInRoleList = new List<UsersInRoleViewModel>();
            foreach (var user in userManager.Users)
            {
                var userInRoleModel = new UsersInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRoleModel.IsSelected = true;
                }
                else
                {
                    userInRoleModel.IsSelected = false;
                }
                usersInRoleList.Add(userInRoleModel);
            }

            return View(usersInRoleList);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UsersInRoleViewModel> modelList, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"The role with Id {roleId} can not be found";
                return View("NotFound");
            }
            for (int i = 0; i < modelList.Count; i++)
            {
                var user = await userManager.FindByIdAsync(modelList[i].UserId);
                IdentityResult result = null;
                if (modelList[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!modelList[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (modelList.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The user with Id {Id} can not be found";
                return View("NotFound");
            }
            var UserClaims = await userManager.GetClaimsAsync(user);
            var UserRoles = await userManager.GetRolesAsync(user);
            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Claims = UserClaims.Select(C => C.Type + " : "+ C.Value).ToList(),
                Roles = UserRoles

            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The user with Id {model.Id} can not be found";
                return View("NotFound");
            }
            user.Email = model.Email;
            user.UserName = model.UserName;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("ListUsers");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The Id {id} can not be found";
                return View("NotFound");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("ListUsers");
            }

        }
        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The Id {userId} can not be found";
                return View("NotFound");
            }

            var model = new List<UserRolesVeiwModel>();
            foreach (var role in roleManager.Roles)
            {
                var userRolesVeiwModel = new UserRolesVeiwModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesVeiwModel.IsSelected = true;
                }
                else
                {
                    userRolesVeiwModel.IsSelected = false;
                }
                model.Add(userRolesVeiwModel);

            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesVeiwModel> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The Id {userId} can not be found";
                return View("NotFound");
            }

            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "roles can not remove user from existing roles");
                return View(model);
            }

            result = await userManager.AddToRolesAsync(user, model.Where(x => x.IsSelected).Select(s => s.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", " can not add selected roles to the user");
                return View(model);
            }


            return RedirectToAction("EditUser", new { id = userId });

        }

        [HttpGet]
        public async Task<IActionResult> ManageUserClaims(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The Id {userId} can not be found";
                return View("NotFound");
            }

            var existingUserClaims = await userManager.GetClaimsAsync(user);
            var model = new UserClaimViewModel
            {
                UserId = userId
            };

            foreach (Claim claim in ClaimsStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type
                };

                if (existingUserClaims.Any(c=> c.Type==claim.Type && c.Value=="true"))
                {
                    userClaim.IsSelected = true;
                }
                model.Claims.Add(userClaim);

            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserClaims(UserClaimViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The Id {model.UserId} can not be found";
                return View("NotFound");
            }

            var claims = await userManager.GetClaimsAsync(user);
            var result = await  userManager.RemoveClaimsAsync(user, claims);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing Claims");
                return View(model);
            }
            result = await userManager.AddClaimsAsync(user, model.Claims.Select(c => new Claim(c.ClaimType, c.IsSelected ? "true":"false")));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected claims to user");
                return View(model);
            }


            return RedirectToAction("EditUser", new { id = model.UserId });
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
