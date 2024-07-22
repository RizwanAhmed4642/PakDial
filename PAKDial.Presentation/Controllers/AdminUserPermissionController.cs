using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminUserPermissionController : Controller
    {
        private readonly IRoleBasedPermissionService roleBasedPermissionService;
        private readonly IModuleService moduleService;

        private readonly IRouteControlsService routeControlsService;
        private readonly IUserBasedPermissionService userBasedPermissionService;
        public AdminUserPermissionController(IRoleBasedPermissionService roleBasedPermissionService
            , IModuleService moduleService, IUserBasedPermissionService userBasedPermissionService,
            IRouteControlsService routeControlsService)
        {
            this.roleBasedPermissionService = roleBasedPermissionService;
            this.moduleService = moduleService;
            this.routeControlsService = routeControlsService;
            this.userBasedPermissionService = userBasedPermissionService;
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult ManagePermission(string UserRoleId)
        {
            string[] UserRoleIds = UserRoleId.Split("||");
            var UserId = UserRoleIds[0];
            var RoleId = UserRoleIds[1];
            var modules = moduleService.GetModulesNotInRoles(RoleId);
            List<decimal> selectedUsers = new List<decimal>();
            foreach (var item in userBasedPermissionService.GetAssignedPermissions(UserId))
                selectedUsers.Add(item.RouteControlId);
            ViewBag.UserId = UserId;
            ViewBag.selectedUsers = selectedUsers;
            return View(modules.ToList());
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult ManageUserPermission(List<UserBasedPermission> userBasedPermissions)
        {
            int result = 0;
            if (userBasedPermissions != null && userBasedPermissions.Count() > 0)
            {
                foreach (var item in userBasedPermissions)
                {
                    item.CreatedBy = HttpContext.Session.GetString("UserId");
                    item.CreatedDate = DateTime.Now;
                    item.UpdatedBy = HttpContext.Session.GetString("UserId");
                    item.UpdatedDate = DateTime.Now;
                }
                result = userBasedPermissionService.AddUpdatePermissions(userBasedPermissions);
            }
            return Json(result);
        }
    }
}