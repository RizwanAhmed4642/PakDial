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
    public class AdminCityController : Controller
    {
        private readonly ICityService cityService;
        private readonly IStateProvinceService stateProvinceService;
        private readonly ICountryService countryService;

        public AdminCityController(ICityService cityService, IStateProvinceService stateProvinceService, ICountryService countryService)
        {
            this.cityService = cityService;
            this.stateProvinceService = stateProvinceService;
            this.countryService = countryService;
        }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult LoadCities()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            CityRequestModel requestModel = new CityRequestModel();
            requestModel.PageNo = start;
            requestModel.PageSize = length;
            requestModel.SearchString = searchValue;
            requestModel.IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false;
            //requestModel.SortColumnName = sortColumnName;
            var results = cityService.GetCities(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.cities });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddCity(City city)
        {
            int Save = 0;
            if (city != null)
            {
                city.CreatedBy = HttpContext.Session.GetString("UserId");
                city.UpdatedBy = HttpContext.Session.GetString("UserId");
                city.CreatedDate = DateTime.Now;
                city.UpdatedDate = DateTime.Now;
                Save = cityService.Add(city);
            }
            return Json(Save);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult GetCityById(decimal Id)
        {
            return Json(cityService.FindById(Id));
        }


        /// <summary>
        /// ///
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateCity(City city)
        {
            city.UpdatedBy = HttpContext.Session.GetString("UserId");
            city.UpdatedDate = DateTime.Now;
            return Json(cityService.Update(city));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteCity(decimal Id)
        {
            return Json(cityService.Delete(Id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult GetStateByCityId(decimal Id)
        {
            return Json(cityService.GetStateByCityId(Id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult GetAllCitiesByStateId(decimal Id)
        {
            return Json(cityService.GetAllCitiesByStates(Id));
        }
    }
}