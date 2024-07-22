using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class AdminZonesController : Controller
    {
        private readonly IZonesService zonesService;
        public AdminZonesController(IZonesService zonesService)
        {
            this.zonesService = zonesService;
        }


        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public JsonResult LoadZones()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            ZonesRequestModel requestModel = new ZonesRequestModel();
            requestModel.PageNo = start;
            requestModel.PageSize = length;
            requestModel.SearchString = searchValue;
            requestModel.IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false;
            var results = zonesService.GetZones(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.Zones });
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddZone(Zones instance)
        {
            decimal Save = 0;
            if (instance != null)
            {
                instance.CreatedBy = HttpContext.Session.GetString("UserId");
                instance.UpdatedBy = HttpContext.Session.GetString("UserId");
                instance.CreatedDate = DateTime.Now;
                instance.UpdatedDate = DateTime.Now;
                Save = zonesService.Add(instance);
            }
            return Json(Save);

        }

        [ValidateAntiForgeryToken]
        public JsonResult GetZonesById(decimal Id)
        {
            return Json(zonesService.FindById(Id));
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateZone(Zones instance)
        {
            instance.UpdatedBy = HttpContext.Session.GetString("UserId");
            instance.UpdatedDate = DateTime.Now;
            return Json(zonesService.Update(instance));
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteZone(decimal Id)
        {
            return Json(zonesService.Delete(Id));
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetAllZones()
        {
            return Json(zonesService.GetAll().ToList());
        }
    }
}