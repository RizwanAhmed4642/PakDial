using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminActiveZoneController : Controller
    {
        private readonly IActiveZoneService activeZoneService;
        private readonly ICityAreaService cityAreaService;
        private readonly ICityService cityService;
        private readonly IStateProvinceService stateProvinceService;
        private readonly IZonesService zonesService;

        public AdminActiveZoneController(IActiveZoneService activeZoneService
            , ICityAreaService cityAreaService, ICityService cityService
            , IStateProvinceService stateProvinceService
            , IZonesService zonesService)
        {
            this.activeZoneService = activeZoneService;
            this.cityAreaService = cityAreaService;
            this.cityService = cityService;
            this.stateProvinceService = stateProvinceService;
            this.zonesService = zonesService;
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }


        [ValidateAntiForgeryToken]
        public JsonResult LoadActiveZone()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            ActiveZonesRequestModel requestModel = new ActiveZonesRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false
            };
            var results = activeZoneService.GetActiveZones(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.VActiveZones });
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddActiveZone(ActiveZone instance)
        {
            decimal Save = 0;
            if (instance != null)
            {
                instance.CreatedBy = HttpContext.Session.GetString("UserId");
                instance.UpdatedBy = HttpContext.Session.GetString("UserId");
                instance.CreatedDate = DateTime.Now;
                instance.UpdatedDate = DateTime.Now;
                Save = activeZoneService.Add(instance);
            }
            return Json(Save);

        }

        [ValidateAntiForgeryToken]
        public JsonResult GetActiveZonesById(decimal Id)
        {
            return Json(activeZoneService.FindById(Id));
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetWrapActiveZoneById(decimal Id)
        {
            return Json(activeZoneService.FindActiveZoneId(Id));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateActiveZone(ActiveZone instance)
        {
            instance.UpdatedBy = HttpContext.Session.GetString("UserId");
            instance.UpdatedDate = DateTime.Now;
            return Json(activeZoneService.Update(instance));
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteActiveZone(decimal Id)
        {
            return Json(activeZoneService.Delete(Id));
        }
        [ValidateAntiForgeryToken]
        public JsonResult GetAllActiveZones()
        {
            return Json(activeZoneService.GetAll().ToList());
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetAllStatesZones()
        {
            VMStateZonesList vMStateZonesList = new VMStateZonesList
            {
                GetStates = stateProvinceService.GetAllStatesKeyValue().ToList(),
                GetZones = zonesService.GetAllZonesKey().ToList()
            };
            return Json(vMStateZonesList);
        }
        [ValidateAntiForgeryToken]
        public JsonResult GetAllCityByStates(decimal StateId)
        {
            return Json(cityService.GetAllCityByStatesKey(StateId));
        }
        [ValidateAntiForgeryToken]
        public JsonResult GetAllCityAreaByCityId(decimal CityId)
        {
            return Json(cityAreaService.GetAllAreaByCityIdKeyValue(CityId));
        }
    }
}