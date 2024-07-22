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
    public class ClientAuthorizationAttribute : ActionFilterAttribute
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISystemUserService systemUserService;
        private readonly ISystemRoleService systemRoleService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICustomerService customerService;

        public ClientAuthorizationAttribute(UserManager<ApplicationUser> userManager,
           RoleManager<ApplicationRole> roleManager,
           SignInManager<ApplicationUser> signInManager,
           ISystemRoleService systemRoleService,
           IHttpContextAccessor httpContextAccessor,
           ISystemUserService systemUserService, ICustomerService customerService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
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
                if(context.HttpContext.Request.IsAjaxRequest())
                {
                    context.Result = new JsonResult(new { success = false, error = "668" });  //Not Login Person
                    return;
                }
                else
                {
                    context.Result = new RedirectResult("~/Home/Login?returnUrl=" + "/" + ControllerName + "/" + ActionName);
                    return;
                }
            }

            else if(context.HttpContext.User.Identity.IsAuthenticated == true)
            {
                if(!string.IsNullOrEmpty(httpContextAccessor.HttpContext.Session.GetString("UserId")) && !string.IsNullOrEmpty(httpContextAccessor.HttpContext.Session.GetString("RoleId")))
                {
                    if (Convert.ToInt32(httpContextAccessor.HttpContext.Session.GetString("UserTypeId")) == 1)
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
                    var Users = systemUserService.FindByEmail(httpContextAccessor.HttpContext.User.Identity.Name);
                    var RoleId = systemRoleService.GetRoleByUserId(Users.Id.ToString());
                    if (Users.UserTypeId == 1)
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
                        var CustomerId = customerService.FindByUserId(Users.Id.ToString()).Id.ToString();
                        httpContextAccessor.HttpContext.Session.SetString("UserId", Users.Id.ToString());
                        httpContextAccessor.HttpContext.Session.SetString("RoleId", RoleId);
                        httpContextAccessor.HttpContext.Session.SetString("CustomerId", CustomerId);
                        httpContextAccessor.HttpContext.Session.SetString("UserTypeId", Users.UserTypeId.ToString());
                    }
                }
            }
           
        }
    }
}
