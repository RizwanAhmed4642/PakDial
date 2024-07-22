using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminTellerOrdersController : Controller
    {
        private readonly IListingPremiumService _listingPremiumService;
        private readonly ICityAreaService _cityAreaService;
        private readonly ICompanyListingsService _companyListingsService;

        public AdminTellerOrdersController(IListingPremiumService listingPremiumService
            , ICityAreaService cityAreaService, ICompanyListingsService companyListingsService)
        {
            _listingPremiumService = listingPremiumService;
            _cityAreaService = cityAreaService;
            _companyListingsService = companyListingsService;
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public JsonResult LoadTellerOrders()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];

            VLoadTellerPackagesRequest requestModel = new VLoadTellerPackagesRequest
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false,
                UserId = HttpContext.Session.GetString("UserId")
            };
            var results = _listingPremiumService.GetLoadTellers(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.VloadTellerPackages });
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult GetUndepositedOrder(decimal Id)
        {
            return Json(_listingPremiumService.GetTellerOrderNotDeposit(Id));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateUnDepositedOrders(decimal Id)
        {
            if (Id > 0)
            {
                VMTellerDeposited request = new VMTellerDeposited
                {
                    Id = Id,
                    UpdatedBy = HttpContext.Session.GetString("UserId"),
                    UpdatedDate = DateTime.Now
                };
                return Json(_listingPremiumService.UpdateTellerOrder(request));
            }
            else
            {
                return Json(null);
            }
        }

        [HttpGet]
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult SaleTellerReport(decimal InvoiceId)
        {
            return View(_listingPremiumService.GetSEReportSale(InvoiceId));
        }
    }
}