using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ResponseModels.SalePersonsOrders;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Presentation.Filters;
using PAKDial.ZongServices.SmsService;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminSubOrdersController : Controller
    {
        private readonly IListingPremiumService _listingPremiumService;
        private readonly ICityAreaService _cityAreaService;
        private readonly ICompanyListingsService _companyListingsService;
        private readonly IService _service;


        public AdminSubOrdersController(IListingPremiumService listingPremiumService
            , ICityAreaService cityAreaService, ICompanyListingsService companyListingsService,
            IService service)
        {
            _listingPremiumService = listingPremiumService;
            _cityAreaService = cityAreaService;
            _companyListingsService = companyListingsService;
            _service = service;
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public JsonResult LoadAllOrders()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];

            VLoadAllOrderPackagesRequest requestModel = new VLoadAllOrderPackagesRequest
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false,
            };
            var results = _listingPremiumService.GetLoadSubAdmin(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.LoadSubAdminOrdersPackages });
        }

        [HttpGet]
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public ActionResult GetOrdersById(decimal Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult GetSubOrdersById(decimal Id)
        {
            return Json(_listingPremiumService.GetSubAdminOrderById(Id));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateOrder(SubAdminOrders instance)
        {
            instance.UpdatedBy = HttpContext.Session.GetString("UserId");
            instance.UpdatedDate = DateTime.Now;
            var Results = _listingPremiumService.UpdateSubOrders(instance);
            if (Results.InvoiceNo > 0)
            {
                OnUpdateAdminOrderMessaging(Results);
            }
            return Json(Results);
        }

        [HttpGet]
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult SalesReport(decimal InvoiceId)
        {
            return View(_listingPremiumService.GetSubAdminReport(InvoiceId));
        }

        // On Update Order By Sales Person Send Email and Message 
        public void OnUpdateAdminOrderMessaging(UpdateSubOrderResponse Results)
        {
            var CustomerMessage = "Dear Customer, " + System.Environment.NewLine+ "Your Package Updated and " + Results.ProcessMessage
                   + " For " + "ListingName = " + Results.CompanyName + " and OrderNo = " + Results.InvoiceNo;

            var CustomerNo = Results.CompanyMobileNo.Substring(1);
            _service.SendMessageAsync("92" + CustomerNo, CustomerMessage);  //Send Message To Customer;
        }
    }
}