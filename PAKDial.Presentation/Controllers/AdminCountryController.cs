using System;
using System.Linq;
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
    public class AdminCountryController : Controller
    {
        private readonly ICountryService countryService;
        public AdminCountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
       
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  Load Country List
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult LoadCountries()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            CountryRequestModel requestModel = new CountryRequestModel();
            requestModel.PageNo = start;
            requestModel.PageSize = length;
            requestModel.SearchString = searchValue;
            requestModel.IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false;
            //requestModel.SortColumnName = sortColumnName;
            var results = countryService.GetCountries(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.Countries });
        }

        /// <summary>
        /// Add a Country by View model
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddCountries(Country country)
        {
            int Save = 0;
            if (country != null)
            {
                country.CreatedBy = HttpContext.Session.GetString("UserId");
                country.UpdatedBy = HttpContext.Session.GetString("UserId");
                country.CreatedDate = DateTime.Now;
                country.UpdatedDate = DateTime.Now;
                Save = countryService.Add(country);
            }
            return Json(Save);

        }

        /// <summary>
        ///  Get country by Country Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult GetCountryById(decimal Id)
        {
            return Json(countryService.FindById(Id));
        }


        /// <summary>
        ///   Update  Country  by Country Id 
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateCountry(Country country)
        {
            country.UpdatedBy = HttpContext.Session.GetString("UserId");
            country.UpdatedDate = DateTime.Now;
            return Json(countryService.Update(country));
        }

        /// <summary>
        ///  delete country by country Id .
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteCountry(decimal Id)
        {
            return Json(countryService.Delete(Id));
        }

        /// <summary>
        ///  Check this Exiting  Code of Country 
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult CheckCountryCodeExit(string countryCode)
        {
            return Json(countryService.FindByCode(countryCode));
        }

        /// <summary>
        ///  This Action Used In Province Controller And City Js  for Get All Country List ....
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult GetAllCountries()
        {
            return Json(countryService.GetAll().ToList());
        }

    }
}