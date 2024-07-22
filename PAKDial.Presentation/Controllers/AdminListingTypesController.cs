using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminListingTypesController : Controller
    {
        #region Prop

        private readonly IListingTypesService _IListingTypesService;
        #endregion

        #region Ctor

        public AdminListingTypesController(IListingTypesService IListingTypesService)
        {
            _IListingTypesService = IListingTypesService;
        }

        #endregion

        #region ActionMethod

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Load Listing Types From DataBase.
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult LoadListingTypes()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            ListingTypesRequestModel requestModel = new ListingTypesRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false
            };
            //requestModel.SortColumnName = sortColumnName;
            var results = _IListingTypesService.GetListingTypes(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.ListingTypes });
        }

        /// <summary>
        /// Add new Listing Types
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]  
        public JsonResult AddListingTypes(ListingTypes listingTypes)
        {
            decimal Result = 0;
            if (listingTypes != null)
            {
                listingTypes.CreatedBy = HttpContext.Session.GetString("UserId");
                listingTypes.UpdatedBy = HttpContext.Session.GetString("UserId");
                listingTypes.CreatedDate = DateTime.Now;
                listingTypes.UpdatedDate = DateTime.Now;
                Result = _IListingTypesService.Add(listingTypes);
            }
            return Json(Result);
        }

        /// <summary>
        /// Update Existing Listing Types Record.
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult EditListingTypes(ListingTypes listingTypes)
        {
            listingTypes.UpdatedBy = HttpContext.Session.GetString("UserId");
            listingTypes.UpdatedDate = DateTime.Now;
            return Json(_IListingTypesService.Update(listingTypes));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteListingTypes(decimal Id)
        {
            return Json(_IListingTypesService.Delete(Id));
        }

        public JsonResult GetListingTypesById(decimal Id)
        {
            return Json(_IListingTypesService.GetById(Id));
        }
        /// <summary>
        /// Get All Listing Types from DataBase.
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        private JsonResult GetAllListingTypes()
        {
            return Json(_IListingTypesService.GetAll().ToList());
        }

        #endregion

    }
}