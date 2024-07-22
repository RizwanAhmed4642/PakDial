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
    public class AdminProvinceController : Controller
    {
        private readonly IStateProvinceService stateProvinceService;
        private readonly ICountryService countryService;

        public AdminProvinceController(IStateProvinceService stateProvinceService, ICountryService countryService)
        {
            this.stateProvinceService = stateProvinceService;
            this.countryService = countryService;
        }

        /// <summary>
        ///  Load List Of Province List........
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
       
        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public JsonResult LoadStates()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            StateProvinceRequestModel requestModel = new StateProvinceRequestModel();
            requestModel.PageNo = start;
            requestModel.PageSize = length;
            requestModel.SearchString = searchValue;
            requestModel.IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false;
            //requestModel.SortColumnName = sortColumnName;
            var results = stateProvinceService.GetStateProvinces(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.stateProvinces });
        }

        /// <summary>
        ///  Add Province View Model
        /// </summary>
        /// <param name="stateProvince"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddProvince(StateProvince stateProvince)
        {
            int Save = 0;
            if (stateProvince != null)
            {
                stateProvince.CreatedBy = HttpContext.Session.GetString("UserId");
                stateProvince.UpdatedBy = HttpContext.Session.GetString("UserId");
                stateProvince.CreatedDate = DateTime.Now;
                stateProvince.UpdatedDate = DateTime.Now;
                Save = stateProvinceService.Add(stateProvince);
            }
            return Json(Save);
        }
        /// <summary>
        ///  Get Provice by  Province Id 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult GetProvinceById(decimal Id)
        {
            return Json(stateProvinceService.FindById(Id));
        }

        /// <summary>
        /// Update Province View Model
        /// </summary>
        /// <param name="stateProvince"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateProvince(StateProvince stateProvince)
        {
            stateProvince.UpdatedBy = HttpContext.Session.GetString("UserId");
            stateProvince.UpdatedDate = DateTime.Now;
            return Json(stateProvinceService.Update(stateProvince));
        }

        /// <summary>
        /// Delete Province by Province Id...
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteProvince(decimal Id)
        {
            return Json(stateProvinceService.Delete(Id));
        }

        /// <summary>
        /// Get ALL State List by country Id  and Use this City Js File
        /// </summary>
        /// <param name="CountryId"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult GetAllStates(decimal CountryId)
        {
            return Json(stateProvinceService.GetAllStatesByCountry(CountryId));
        }

    }
}