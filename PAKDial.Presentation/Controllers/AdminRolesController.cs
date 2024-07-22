using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.IdentityManagement;
using PAKDial.Domains.RequestModels;
using PAKDial.Interfaces.CommonServices;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminRolesController : Controller
    {
        private readonly ISystemRoleService systemRoleService;
        public AdminRolesController(ISystemRoleService systemRoleService)
        {
            this.systemRoleService = systemRoleService;
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        public JsonResult LoadRoles()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            RoleRequestModel requestModel = new RoleRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false
            };
            //requestModel.SortColumnName = sortColumnName;
            var results = systemRoleService.GetRoles(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.AspRoles });
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AddRole(ApplicationRole role)
        {
            int Save = 0;
            if (role != null)
            {
                role.NormalizedName = role.Name.ToUpper().Trim();
                role.CreatedBy = HttpContext.Session.GetString("UserId");
                role.UpdatedBy = HttpContext.Session.GetString("UserId");
                role.CreatedDate = DateTime.Now;
                role.UpdatedDate = DateTime.Now;
                Save = await systemRoleService.Add(role);
            }
            return Json(Save);

        }
        [ValidateAntiForgeryToken]
        public JsonResult GetRoleById(string Id)
        {
            return Json(systemRoleService.FindById(Id));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateRole(ApplicationRole role)
        {
            int Save = 0;
            if (role != null)
            {
                role.NormalizedName = role.Name.ToUpper().Trim();
                role.UpdatedBy = HttpContext.Session.GetString("UserId");
                role.UpdatedDate = DateTime.Now;
                Save = systemRoleService.Update(role);
            }
            return Json(Save);
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteRole(string Id)
        {
            int Save = 0;
            if (Id != null)
            {
                Save = await systemRoleService.Delete(Id);
            }
            return Json(Save);
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetAllRoles()
        {
            return Json(systemRoleService.GetAll().ToList());
        }
    }
}