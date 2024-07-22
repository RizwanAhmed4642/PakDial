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

    public class AdminDesignationController : Controller
    {
        private readonly IDesignationService designationService;
        public AdminDesignationController(IDesignationService designationService)
        {
            this.designationService = designationService;
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        public JsonResult LoadDesignations()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            DesignationRequestModel requestModel = new DesignationRequestModel();
            requestModel.PageNo = start;
            requestModel.PageSize = length;
            requestModel.SearchString = searchValue;
            requestModel.IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false;
            //requestModel.SortColumnName = sortColumnName;
            var results = designationService.GetDesignations(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.designations });
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddDesignation(Designation designation)
        {
            int Save = 0;
            if (designation != null)
            {
                designation.CreatedBy = HttpContext.Session.GetString("UserId");
                designation.UpdatedBy = HttpContext.Session.GetString("UserId");
                designation.CreatedDate = DateTime.Now;
                designation.UpdatedDate = DateTime.Now;
                Save = designationService.Add(designation);
            }
            return Json(Save);

        }

        [ValidateAntiForgeryToken]
        public JsonResult GetDesignationById(decimal Id)
        {
            return Json(designationService.FindById(Id));
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateDesignation(Designation designation)
        {
            designation.UpdatedBy = HttpContext.Session.GetString("UserId");
            designation.UpdatedDate = DateTime.Now;
            return Json(designationService.Update(designation));
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteDesignation(decimal Id)
        {
            return Json(designationService.Delete(Id));
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetAllDesignations()
        {
            return Json(designationService.GetAll().ToList());
        }
    }
}