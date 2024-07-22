using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.PakDialServices.IListingQuery;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    public class AdminQueryTrackController : Controller
    {
        private readonly IListingQueryTrackService _listingQueryTrackService;
        public AdminQueryTrackController(IListingQueryTrackService listingQueryTrackService)
        {
            _listingQueryTrackService = listingQueryTrackService;
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index(decimal Id)
        {
            return View(new VMAssignPackageModel { Id = Id });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult LoadQueryTrack(decimal ListingId)
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            ListingQueryTrackRequestModel requestModel = new ListingQueryTrackRequestModel();
            requestModel.PageNo = start;
            requestModel.PageSize = length;
            requestModel.SearchString = searchValue;
            requestModel.IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false;
            requestModel.ListingId = ListingId;
            //requestModel.SortColumnName = sortColumnName;
            var results = _listingQueryTrackService.GetListingQueryTrack(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.QueryTrack });
        }

        public IActionResult  ShowPdf()
        {
            return View();
        }
    }
}