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
    public class AdminZMOrderAssigningController : Controller
    {
        private readonly IListingPremiumService _listingPremiumService;

        public AdminZMOrderAssigningController(IListingPremiumService listingPremiumService)
        {
            _listingPremiumService = listingPremiumService;
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        public JsonResult LoadZMOrders()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];

            VLoadZoneManagerTransferRequest requestModel = new VLoadZoneManagerTransferRequest
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false,
                UserId = HttpContext.Session.GetString("UserId")
            };
            var results = _listingPremiumService.GetLoadZoneTransferOrders(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.vLoadZoneManagers });
        }
        [ValidateAntiForgeryToken]
        public IActionResult GetUserByZM()
        {
            return Json(_listingPremiumService.GetEmployeesByZoneManagerId(HttpContext.Session.GetString("UserId")));
        }

        [HttpGet]
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public ActionResult GetUnAssignedOrders(decimal Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult GetUnAssignedOrder(decimal Id)
        {
            return Json(_listingPremiumService.GetNotAssignedOrders(Id));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateAssigningOrders(VMZoneManagerTransfer instance)
        {
            instance.AssignedFrom = HttpContext.Session.GetString("UserId");
            instance.AssignedDate = DateTime.Now;
            return Json(_listingPremiumService.AssigningOrders(instance));
        }
    }
}