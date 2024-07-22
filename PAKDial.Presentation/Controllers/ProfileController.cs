using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    public class ProfileUserController : Controller
    {


        #region Prop

        private readonly ICompanyListingsService _companyListingsService;

        #endregion

        #region Ctor

        public ProfileUserController(ICompanyListingsService companyListingsService)
        {
            _companyListingsService = companyListingsService;
        }

        #endregion

        #region Method

        public IActionResult Index()
        {
            return View();
        }

        [ServiceFilter(typeof(ClientAuthorizationAttribute))]
        public IActionResult Profile()
        {
            ViewBag.CustomerId = Convert.ToDecimal(HttpContext.Session.GetString("CustomerId"));
            return View();
        }

        [ServiceFilter(typeof(ClientAuthorizationAttribute))]
        public JsonResult GetCompanyListingByUserId()
        {
            return Json(_companyListingsService.GetCompanyListingsByUserId(Convert.ToDecimal(HttpContext.Session.GetString("CustomerId"))));
        }

        #endregion

    }
}