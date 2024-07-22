using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminListingPackagesController : Controller
    {
        private readonly IListingPackagesService listingPackagesService;
        private readonly IPackagePricesService packagePricesService;
        public AdminListingPackagesController(IListingPackagesService listingPackagesService
            , IPackagePricesService packagePricesService)
        {
            this.listingPackagesService = listingPackagesService;
            this.packagePricesService = packagePricesService;
        }


        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }


        [ValidateAntiForgeryToken]
        public IActionResult LoadListingPackages()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            ListingPackagesRequestModel requestModel = new ListingPackagesRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false
            };
            //requestModel.SortColumnName = sortColumnName;
            var results = listingPackagesService.GetListingPackages(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.ListingPackages });
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddListingPackages(ListingPackageViewModel instance)
        {
            decimal Save = 0;
            if (instance != null)
            {
                instance.CreatedBy = HttpContext.Session.GetString("UserId");
                instance.UpdatedBy = HttpContext.Session.GetString("UserId");
                instance.CreatedDate = DateTime.Now;
                instance.UpdatedDate = DateTime.Now;
                Save = listingPackagesService.Add(instance);
            }
            return Json(Save);

        }

        [ValidateAntiForgeryToken]
        public JsonResult GetListingPackageById(decimal Id)
        {
            return Json(listingPackagesService.GetById(Id));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateListingPackages(ListingPackageViewModel instance)
        {
            instance.UpdatedBy = HttpContext.Session.GetString("UserId");
            instance.UpdatedDate = DateTime.Now;
            return Json(listingPackagesService.UpdatePackages(instance));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteListingPackages(decimal Id)
        {
            return Json(listingPackagesService.DeletePackage(Id));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult PackagesPricesList(decimal Id)
        {
            return View(packagePricesService.GetAllPackagePrices(Id));
        }


    }
}