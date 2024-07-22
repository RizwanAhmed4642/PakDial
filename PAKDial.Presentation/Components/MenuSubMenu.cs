using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PAKDial.Domains.IdentityManagement;
using PAKDial.Domains.ResponseModels;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.PakDialServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAKDial.Presentation.Components
{
    public class MenuSubMenu : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISystemUserService systemUserService;
        private readonly ISystemRoleService systemRoleService;
        private readonly IEmployeeService employeeService;

        public MenuSubMenu(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager, ISystemUserService systemUserService,
            ISystemRoleService systemRoleService, IEmployeeService employeeService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.systemUserService = systemUserService;
            this.systemRoleService = systemRoleService;
            this.employeeService = employeeService;

        }
        public IViewComponentResult Invoke()
        {
            List<VUserLoginRUMenu> ViewCheckResults = new List<VUserLoginRUMenu>();
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")) && !string.IsNullOrEmpty(HttpContext.Session.GetString("RoleId")))
            {
                LoginMenuResponseModel MenuList = HttpContext.Session.GetString("UserRoleMenu") != null ? JsonConvert.DeserializeObject<LoginMenuResponseModel>(HttpContext.Session.GetString("UserRoleMenu")) : null;    
                if (MenuList != null)
                {
                    var CheckResults = MenuList.UserLoginRUMenu.ToList();
                    if (CheckResults != null)
                    {
                        ViewCheckResults = CheckResults;
                    }
                }
                else
                {
                    var MainCheckResults = systemUserService.GetRoleUserMenu(HttpContext.Session.GetString("UserId"), HttpContext.Session.GetString("RoleId"));
                    if (MainCheckResults != null)
                    {
                        if (MainCheckResults.UserLoginRUMenu != null && MainCheckResults.UserLoginRUMenu.Count() > 0)
                        {
                            var CheckResults = MainCheckResults.UserLoginRUMenu.ToList();
                            if (CheckResults != null)
                            {
                                HttpContext.Session.SetString("UserRoleMenu", JsonConvert.SerializeObject(MainCheckResults));
                                ViewCheckResults = CheckResults;
                            }
                        }
                    }
                }
            }
            else if(HttpContext.User.Identity.IsAuthenticated == true)
            {
                var Users = systemUserService.FindByEmail(HttpContext.User.Identity.Name);
                var RoleId = systemRoleService.GetRoleByUserId(Users.Id.ToString());
                var EmployeeId = employeeService.FindByUserId(Users.Id.ToString()).Id.ToString();

                var MainCheckResults = systemUserService.GetRoleUserMenu(Users.Id.ToString(), RoleId);
                if (MainCheckResults != null)
                {
                    if (MainCheckResults.UserLoginRUMenu != null && MainCheckResults.UserLoginRUMenu.Count() > 0)
                    {
                        var CheckResults = MainCheckResults.UserLoginRUMenu.ToList();
                        if (CheckResults != null)
                        {
                            HttpContext.Session.SetString("UserId", Users.Id.ToString());
                            HttpContext.Session.SetString("RoleId", RoleId);
                            HttpContext.Session.SetString("UserTypeId", Users.UserTypeId.ToString());
                            HttpContext.Session.SetString("EmployeeId", EmployeeId);
                            HttpContext.Session.SetString("UserRoleMenu", JsonConvert.SerializeObject(MainCheckResults));
                            ViewCheckResults = CheckResults;
                        }
                    }
                }
            }
            return View(ViewCheckResults);
        }
    }
}
