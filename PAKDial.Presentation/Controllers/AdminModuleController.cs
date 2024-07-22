using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminModuleController : Controller
    {
        private readonly IModuleService moduleService;
        public AdminModuleController(IModuleService moduleService)
        {
            this.moduleService = moduleService;
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        public JsonResult LoadModules()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            ModulesRequestModel requestModel = new ModulesRequestModel();
            requestModel.PageNo = start;
            requestModel.PageSize = length;
            requestModel.SearchString = searchValue;
            requestModel.IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false;
            //requestModel.SortColumnName = sortColumnName;
            var results = moduleService.GetModules(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.Modules });
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddModule(Modules module)
        {
            int Save = 0;
            if (module != null)
            {
                module.CreatedBy = HttpContext.Session.GetString("UserId");
                module.UpdatedBy = HttpContext.Session.GetString("UserId");
                module.CreatedDate = DateTime.Now;
                module.UpdatedDate = DateTime.Now;
                Save = moduleService.Add(module);
            }
            return Json(Save);

        }
        [ValidateAntiForgeryToken]
        public JsonResult GetModuleById(decimal Id)
        {
            return Json(moduleService.FindById(Id));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateModule(Modules module)
        {
            module.UpdatedBy = HttpContext.Session.GetString("UserId");
            module.UpdatedDate = DateTime.Now;
            return Json(moduleService.Update(module));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteModule(decimal Id)
        {
            return Json(moduleService.Delete(Id));
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetAllModules()
        {
            return Json(moduleService.GetAll().ToList());
        }
    }
}