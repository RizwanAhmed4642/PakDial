using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using PAKDial.Presentation.Filters;


namespace PAKDial.Presentation.Controllers
{
    [Authorize]

    public class AdminAssigningPackagesController : Controller
    {
        private readonly IListingPremiumService _listingPremiumService;
        public AdminAssigningPackagesController(IListingPremiumService listingPremiumService)
        {
            _listingPremiumService = listingPremiumService;
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index(decimal Id)
        {
            return View(new VMAssignPackageModel {Id=Id });
        }

        [ValidateAntiForgeryToken]
        public JsonResult LoadAssignedListingPackages(decimal ListingId)
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            AssignListingPackageRequestModel requestModel = new AssignListingPackageRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false,
                ListingId = ListingId
            };
            //requestModel.SortColumnName = sortColumnName;
            var results = _listingPremiumService.Get(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.AssignListingPackages });
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetPackgesModes()
        {
            return Json(_listingPremiumService.GetPakagesModes());
        }
    }
}