using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminRolePermissionController : Controller
    {
        private readonly IRoleBasedPermissionService roleBasedPermissionService;
        private readonly IModuleService moduleService;
        private readonly IRouteControlsService routeControlsService;
        public AdminRolePermissionController(IRoleBasedPermissionService roleBasedPermissionService
            , IModuleService moduleService,
            IRouteControlsService routeControlsService)
        {
            this.roleBasedPermissionService = roleBasedPermissionService;
            this.moduleService = moduleService;
            this.routeControlsService = routeControlsService;
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult ManagePermission(string Id)
        {
            var modules = moduleService.GetAllIncludeRoutes();
            List<decimal> selectedRoles = new List<decimal>();
            foreach (var item in roleBasedPermissionService.GetAll().Where(r => r.RoleId == Id))
                selectedRoles.Add(item.RouteControlId);
            ViewBag.RoleId = Id;
            ViewBag.SelectedRoles = selectedRoles;
            return View(modules.ToList());
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult ManageRole(List<RoleBasedPermission> rolePermissions)
        {
            int result = 0;
            if (rolePermissions != null && rolePermissions.Count() > 0)
            {
                foreach (var item in rolePermissions)
                {
                    item.CreatedBy = HttpContext.Session.GetString("UserId");
                    item.CreatedDate = DateTime.Now;
                    item.UpdatedBy = HttpContext.Session.GetString("UserId");
                    item.UpdatedDate = DateTime.Now;
                }
                result = roleBasedPermissionService.AddUpdatePermissions(rolePermissions);
            }
            return Json(result);
        }
    }
}