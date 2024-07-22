using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Interfaces.CommonServices;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminCityAreasController : Controller
    {
        private readonly ICityAreaService cityAreaService;
        private readonly ICityService cityService;
        private readonly IStateProvinceService stateProvinceService;
        private readonly ICountryService countryService;

        public AdminCityAreasController(ICityAreaService cityAreaService, ICityService cityService, IStateProvinceService stateProvinceService, ICountryService countryService)
        {
            this.cityAreaService = cityAreaService;
            this.cityService = cityService;
            this.stateProvinceService = stateProvinceService;
            this.countryService = countryService;
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        public JsonResult LoadCityAreas()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            CityAreaRequestModel requestModel = new CityAreaRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false
            };
            //requestModel.SortColumnName = sortColumnName;
            var results = cityAreaService.GetCitiesArea(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.cityAreas });
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddCityArea(CityArea cityArea)
        {
            int Save = 0;
            if (cityArea != null)
            {
                cityArea.CreatedBy = HttpContext.Session.GetString("UserId");
                cityArea.UpdatedBy = HttpContext.Session.GetString("UserId");
                cityArea.CreatedDate = DateTime.Now;
                cityArea.UpdatedDate = DateTime.Now;
                Save = cityAreaService.Add(cityArea);
            }
            return Json(Save);
        }
        [ValidateAntiForgeryToken]
        public JsonResult GetCityAreaById(decimal Id)
        {
            return Json(cityAreaService.FindById(Id));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateCityArea(CityArea cityArea)
        {
            cityArea.UpdatedBy = HttpContext.Session.GetString("UserId");
            cityArea.UpdatedDate = DateTime.Now;
            return Json(cityAreaService.Update(cityArea));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteCityArea(decimal Id)
        {
            return Json(cityAreaService.Delete(Id));
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetAllCityAreaByCityId(decimal CityId)
        {
            return Json(cityAreaService.GetAllAreasByCity(CityId));
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetSCByCityAreaId(decimal Id)
        {
            return Json(cityAreaService.GetSCByCityAreaId(Id));
        }
    }
}