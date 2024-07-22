using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Interfaces.Repository;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminBusinessTypesController : Controller
    {


        private readonly IBusinessTypesService businessTypesService;
        public AdminBusinessTypesController(IBusinessTypesService businessTypesService)
        {
            this.businessTypesService = businessTypesService;
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public JsonResult LoadBusinessTypes()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];

            BusinessTypesRequestModel requestModel = new BusinessTypesRequestModel();
            requestModel.PageNo = start;
            requestModel.PageSize = length;
            requestModel.SearchString = searchValue;
            requestModel.IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false;
            var results = businessTypesService.GetBusinessTypes(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.Businesstypes });
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddBusinessTypes(BusinessTypes instance)
        {
            decimal Save = 0;
            if (instance != null)
            {
                instance.CreatedBy = HttpContext.Session.GetString("UserId");
                instance.UpdatedBy = HttpContext.Session.GetString("UserId");
                instance.CreatedDate = DateTime.Now;
                instance.UpdatedDate = DateTime.Now;
                Save = businessTypesService.Add(instance);
            }
            return Json(Save);

        }

        [ValidateAntiForgeryToken]
        public JsonResult GetBusinessTypesById(decimal Id)
        {
            return Json(businessTypesService.FindById(Id));
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateBusinessTypes(BusinessTypes instance)
        {
            instance.UpdatedBy = HttpContext.Session.GetString("UserId");
            instance.UpdatedDate = DateTime.Now;
            return Json(businessTypesService.Update(instance));
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteBusinessTypes(decimal Id)
        {
            return Json(businessTypesService.Delete(Id));
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetAllBusinessTypes()
        {
            return Json(businessTypesService.GetAll().ToList());
        }
    }
}