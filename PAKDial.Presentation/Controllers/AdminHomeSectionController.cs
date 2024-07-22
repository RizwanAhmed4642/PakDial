using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminHomeSectionController : Controller
    {
        private readonly IHomeSectionCategoryService homeSectionCategoryService;
        public AdminHomeSectionController(IHomeSectionCategoryService homeSectionCategoryService)
        {
            this.homeSectionCategoryService = homeSectionCategoryService;
        }


        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }


        [ValidateAntiForgeryToken]
        public JsonResult LoadHomeSection()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];

            HomeSectionCategoryRequestModel requestModel = new HomeSectionCategoryRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false
            };
            //requestModel.SortColumnName = sortColumnName;
            var results = homeSectionCategoryService.GetHomeSection(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.HomeSectionCategories });
        }


        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddHomeSection(HomeSectionCategory homeSection)
        {
            int Save = 0;
            if (homeSection != null)
            {
                homeSection.CreatedBy = HttpContext.Session.GetString("UserId");
                homeSection.UpdatedBy = HttpContext.Session.GetString("UserId");
                homeSection.CreatedDate = DateTime.Now;
                homeSection.UpdatedDate = DateTime.Now;
                Save = homeSectionCategoryService.Add(homeSection);
            }
            return Json(Save);

        }
        [ValidateAntiForgeryToken]
        public JsonResult GetHomeSectionById(decimal Id)
        {
            return Json(homeSectionCategoryService.FindById(Id));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateHomeSection(HomeSectionCategory homeSection)
        {
            homeSection.UpdatedBy = HttpContext.Session.GetString("UserId");
            homeSection.UpdatedDate = DateTime.Now;
            return Json(homeSectionCategoryService.Update(homeSection));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteHomeSection(decimal Id)
        {
            return Json(homeSectionCategoryService.Delete(Id));
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetAllHomeSection()
        {
            return Json(homeSectionCategoryService.GetAll().ToList());
        }
    }
}