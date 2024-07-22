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
using PAKDial.Presentation.Services;
using PAKDial.ZongServices.SmsService;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]

    public class AdminSeOrdersController : Controller
    {
        private readonly IListingPremiumService _listingPremiumService;
        private readonly ICityAreaService _cityAreaService;
        private readonly ICompanyListingsService _companyListingsService;
        private readonly IEmailSender _emailSender;
        private readonly IService _service;



        public AdminSeOrdersController(IListingPremiumService listingPremiumService
            , ICityAreaService cityAreaService, ICompanyListingsService companyListingsService, IEmailSender emailSender
            , IService service)
        {
            _listingPremiumService = listingPremiumService;
            _cityAreaService = cityAreaService;
            _companyListingsService = companyListingsService;
            _emailSender = emailSender;
            _service = service;
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public JsonResult LoadSeOrders()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];

            VLoadSalesExectivePackagesRequest requestModel = new VLoadSalesExectivePackagesRequest
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false,
                UserId = HttpContext.Session.GetString("UserId")
            };
            var results = _listingPremiumService.GetLoadSalesManagers(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.Vloadsalesexectivepackages });
        }
        [ValidateAntiForgeryToken]
        public IActionResult GetPaymentModeandPackage()
        {
            return Json(_listingPremiumService.GetModeandPackagesResponses());
        }
        [ValidateAntiForgeryToken]
        public JsonResult SearchCombineCityArea(string search, string pageNo, string pageSize)
        {
            CombineRegionsRequestModel request = new CombineRegionsRequestModel
            {
                SearchString = search,
                PageNo = Convert.ToInt32(pageNo),
                PageSize = Convert.ToInt32(pageSize),
                UserId = HttpContext.Session.GetString("UserId"),
            };
            var results = _cityAreaService.GetCombineRegionUserId(request);
            return Json(results);
        }
        [ValidateAntiForgeryToken]
        public JsonResult SearchCompanyByArea(string search, string pageNo, string pageSize, decimal CityAreaId)
        {
            if (CityAreaId > 0)
            {
                CompanyKeyValueSearchRequestModel request = new CompanyKeyValueSearchRequestModel
                {
                    SearchString = search,
                    PageNo = Convert.ToInt32(pageNo),
                    PageSize = Convert.ToInt32(pageSize),
                    CityAreaId = CityAreaId,
                };
                var results = _companyListingsService.GetCompanySearchOnAreaBases(request);
                return Json(results);
            }
            else
                return Json(null);
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult AddSaleExOrderGet()
        {
            return View();
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddSaleExOrder(VMSaleExCreate instance)
        {
            instance.CreatedBy = HttpContext.Session.GetString("UserId");
            instance.CreatedDate = DateTime.Now;
            var Results = _listingPremiumService.AddOrdersSaleManager(instance);
            if(!string.IsNullOrEmpty(Results.CompanyName))
            {
                OnCreateSEOrderMessaging(Results);
            }
            return Json(Results);
        }

        [HttpGet]
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult SaleExReport(decimal InvoiceId)
        {
            return View(_listingPremiumService.GetSEReportSale(InvoiceId));
        }
        [HttpGet]
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult GetCollectSaleExOrder(decimal InvoiceId)
        {
            ViewBag.InvoiceId = InvoiceId;
            return View();
        }

        [ValidateAntiForgeryToken]
        public IActionResult GetPaymentModes()
        {
            return Json(_listingPremiumService.GetPaymentsModeResponses());
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult GetCollectSaleExOrders(decimal InvoiceId)
        {
            return Json(_listingPremiumService.GetSePendingOrdersfor(InvoiceId));
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateCollectSaleExOrder(VMSaleExCollect instance)
        {
            instance.UpdatedBy = HttpContext.Session.GetString("UserId");
            instance.UpdatedDate = DateTime.Now;
            var Results = _listingPremiumService.CollectOrdersSaleManager(instance);
            if (!string.IsNullOrEmpty(Results.CompanyName))
            {
                OnUpdateSEOrderMessaging(Results);
            }
            return Json(Results);
        }

        // On Create Order By Sales Person Send Email and Message 
        public void OnCreateSEOrderMessaging(AddSeOrderResponse Results)
        {
            var CustomerMessage = "Dear Customer, " + System.Environment.NewLine + "Your " + Results.ProcessMessage
                   + " For " + "ListingName = " + Results.CompanyName + " OrderNo = " + Results.InvoiceNo
                   + " Package = " + Results.AssignedPackage + " Charges =" + Results.PackageCost
                   + " For Months = " + Results.PackageMonths + " And Payment Mode = " + Results.PaymentMode;

            var EmployeeMessage = "Dear Employee, " + System.Environment.NewLine+ Results.ProcessMessage
                + " For " + "ListingName = " + Results.CompanyName + " OrderNo = " + Results.InvoiceNo
                + " Package = " + Results.AssignedPackage + " Charges =" + Results.PackageCost
                + " For Months = " + Results.PackageMonths + " And Payment Mode = " + Results.PaymentMode;

            var FinanceEmailMessage = "    " + Results.ProcessMessage
                + " For " + "ListingName = " + Results.CompanyName + " OrderNo = " + Results.InvoiceNo
                + " Package = " + Results.AssignedPackage + " Charges =" + Results.PackageCost
                + " For Months = " + Results.PackageMonths + " Payment Mode = " + Results.PaymentMode
                + " Created By Sale Person and EmployeeNo = " + Results.SalePersonNo;

            var EmployeeNo = Results.SalePersonNo.Substring(1);
            var CustomerNo = Results.CompanyMobileNo.Substring(1);
            _service.SendMessageAsync("92" + CustomerNo, CustomerMessage);  //Send Message To Customer;
            _service.SendMessageAsync("92" + EmployeeNo, EmployeeMessage);  //Send Message To Employee;
            //_emailSender.SendEmailAsync("PakDialMedia@gmail.com", "Dear Finance, " + System.Environment.NewLine , FinanceEmailMessage); //Send Email Message To Finance Department;
        }

        // On Update Order By Sales Person Send Email and Message 
        public void OnUpdateSEOrderMessaging(UpdateSeOrderResponse Results)
        {
            var CustomerMessage = "Dear Customer, " + System.Environment.NewLine + "Your Package Updated and " + Results.ProcessMessage
                   + " For " + "ListingName = " + Results.CompanyName + " OrderNo = " + Results.InvoiceNo
                   + " Package = " + Results.AssignedPackage + " Charges =" + Results.PackageCost
                   + " For Months = " + Results.PackageMonths + " And Payment Mode = " + Results.PaymentMode
                   + ".Let's evolve togther!";

            var EmployeeMessage = "Dear Employee, " + System.Environment.NewLine+ "Package Updated and " + Results.ProcessMessage
                + " For " + "ListingName = " + Results.CompanyName + " OrderNo = " + Results.InvoiceNo
                + " Package = " + Results.AssignedPackage + " Charges =" + Results.PackageCost
                + " For Months = " + Results.PackageMonths + " And Payment Mode = " + Results.PaymentMode;

            var FinanceEmailMessage = "Package Updated and " + Results.ProcessMessage
                + " For " + "ListingName = " + Results.CompanyName + " OrderNo = " + Results.InvoiceNo
                + " Package = " + Results.AssignedPackage + " Charges =" + Results.PackageCost
                + " For Months = " + Results.PackageMonths + " Payment Mode = " + Results.PaymentMode
                + " Updated By Sale Person and By EmployeeNo = " + Results.SalePersonNo;

            var EmployeeNo = Results.SalePersonNo.Substring(1);
            var CustomerNo = Results.CompanyMobileNo.Substring(1);
            _service.SendMessageAsync("92" + CustomerNo, CustomerMessage);  //Send Message To Customer;
            _service.SendMessageAsync("92" + EmployeeNo, EmployeeMessage);  //Send Message To Employee;
           // _emailSender.SendEmailAsync("PakDialMedia@gmail.com", "Dear Finance, " + System.Environment.NewLine, FinanceEmailMessage); //Send Email Message To Finance Department;
        }
    }
}