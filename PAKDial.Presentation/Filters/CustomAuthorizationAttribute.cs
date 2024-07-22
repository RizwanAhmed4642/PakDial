using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using PAKDial.Domains.IdentityManagement;
using PAKDial.Domains.ResponseModels;
using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.PakDialServices;
using System;
using System.Linq;

namespace PAKDial.Presentation.Filters
{
    public class CustomAuthorizationAttribute : ActionFilterAttribute
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISystemUserService systemUserService;
        private readonly ISystemRoleService systemRoleService;
        private readonly IEmployeeService employeeService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICustomerService customerService;

        public CustomAuthorizationAttribute(UserManager<ApplicationUser> userManager,
           RoleManager<ApplicationRole> roleManager,
           SignInManager<ApplicationUser> signInManager,
           IEmployeeService employeeService,
           ISystemRoleService systemRoleService,
           IHttpContextAccessor httpContextAccessor,
           ISystemUserService systemUserService, ICustomerService customerService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            this.employeeService = employeeService;
            this.systemRoleService = systemRoleService;
            this.httpContextAccessor = httpContextAccessor;
            this.systemUserService = systemUserService;
            this.customerService = customerService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request == null || context == null || context.HttpContext == null)
            {
                return;
            }

            var ControllerName = context.RouteData.Values["controller"].ToString();
            var ActionName = context.RouteData.Values["action"].ToString();

            if (context.HttpContext.User.Identity.IsAuthenticated == false)
            {
                context.Result = new RedirectResult("~/Account/Login?returnUrl=" + "/" + ControllerName + "/" + ActionName);
                return;
            }

            if (!string.IsNullOrEmpty(httpContextAccessor.HttpContext.Session.GetString("UserId")) && !string.IsNullOrEmpty(httpContextAccessor.HttpContext.Session.GetString("RoleId")))
            {
                if (Convert.ToInt32(httpContextAccessor.HttpContext.Session.GetString("UserTypeId")) == 2)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Account" }, { "action", "AccessDenied" } });
                    return;
                }
                LoginMenuResponseModel MenuList = httpContextAccessor.HttpContext.Session.GetString("UserRoleMenu") != null ? (JsonConvert.DeserializeObject<LoginMenuResponseModel>(httpContextAccessor.HttpContext.Session.GetString("UserRoleMenu"))) : null;
                if (MenuList != null)
                {
                    var CheckResults = MenuList.UserLoginRUMenu.Where(c => c.Controller.ToLower().Trim() == ControllerName.ToLower().Trim() && c.Action.ToLower().Trim() == ActionName.ToLower().Trim()).FirstOrDefault();
                    if (CheckResults == null)
                    {
                        if (context.HttpContext.Request.IsAjaxRequest())
                        {
                            context.Result = new JsonResult(new { success = false, error = "403" });
                            return;
                        }
                        else
                        {
                            context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Account" }, { "action", "AccessDenied" } });
                            return;
                        }

                    }
                }
                else
                {
                    var MainCheckResults = systemUserService.GetRoleUserMenu(httpContextAccessor.HttpContext.Session.GetString("UserId").ToString(), httpContextAccessor.HttpContext.Session.GetString("RoleId").ToString());
                    if (MainCheckResults.UserLoginRUMenu.Count() < 1)
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Account" }, { "action", "AccessDenied" } });
                        return;
                    }
                    else
                    {
                        if (MainCheckResults.UserLoginRUMenu != null && MainCheckResults.UserLoginRUMenu.Count() > 0)
                        {
                            var CheckResults = MainCheckResults.UserLoginRUMenu.Where(c => c.Controller.ToLower().Trim() == ControllerName.ToLower().Trim() && c.Action.ToLower().Trim() == ActionName.ToLower().Trim()).FirstOrDefault();
                            if (CheckResults == null)
                            {
                                if (context.HttpContext.Request.IsAjaxRequest())
                                {
                                    context.Result = new JsonResult(new { success = false, error = "403" });
                                    return;
                                }
                                else
                                {
                                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Account" }, { "action", "AccessDenied" } });
                                    return;
                                }
                            }
                            else
                            {
                                httpContextAccessor.HttpContext.Session.SetString("UserRoleMenu", JsonConvert.SerializeObject(MainCheckResults));
                            }
                        }
                    }
                }
            }
            else
            {
                var Users = systemUserService.FindByEmail(httpContextAccessor.HttpContext.User.Identity.Name);
                var RoleId = systemRoleService.GetRoleByUserId(Users.Id.ToString());
                var EmployeeId = employeeService.FindByUserId(Users.Id.ToString()).Id.ToString();
                if (Users.UserTypeId == 2)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Account" }, { "action", "AccessDenied" } });
                    return;
                }
                var MainCheckResults = systemUserService.GetRoleUserMenu(Users.Id.ToString(), RoleId);
                if (MainCheckResults == null)
                {
                    if (context.HttpContext.Request.IsAjaxRequest())
                    {
                        context.Result = new JsonResult(new { success = false, error = "403" });
                        return;
                    }
                    else
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Account" }, { "action", "AccessDenied" } });
                        return;
                    }
                }
                else
                {
                    if (MainCheckResults.UserLoginRUMenu != null && MainCheckResults.UserLoginRUMenu.Count() > 0)
                    {
                        var CheckResults = MainCheckResults.UserLoginRUMenu.Where(c => c.Controller.ToLower().Trim() == ControllerName.ToLower().Trim() && c.Action.ToLower().Trim() == ActionName.ToLower().Trim()).FirstOrDefault();
                        if (CheckResults == null)
                        {
                            if (context.HttpContext.Request.IsAjaxRequest())
                            {
                                context.Result = new JsonResult(new { success = false, error = "403" });
                                return;
                            }
                            else
                            {
                                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Account" }, { "action", "AccessDenied" } });
                                return;
                            }
                        }
                        else
                        {
                            httpContextAccessor.HttpContext.Session.SetString("UserId", Users.Id.ToString());
                            httpContextAccessor.HttpContext.Session.SetString("RoleId", RoleId);
                            httpContextAccessor.HttpContext.Session.SetString("EmployeeId", EmployeeId);
                            httpContextAccessor.HttpContext.Session.SetString("UserTypeId", Users.UserTypeId.ToString());

                            httpContextAccessor.HttpContext.Session.SetString("UserRoleMenu", JsonConvert.SerializeObject(MainCheckResults));
                        }
                    }
                    else
                    {
                        context.Result = new RedirectResult("~/Account/Login?returnUrl" + "~/" + ControllerName + "/" + ActionName);
                        return;
                    }
                }

            }
        }
    }
}
