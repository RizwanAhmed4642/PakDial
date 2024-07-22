using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Interfaces.CommonServices;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminMMCategoryController : Controller
    {
        private readonly IMainMenuCategoryService mainMenuCategoryService;
        private readonly ICategoryTypesService categoryTypesService;
        private readonly IHostingEnvironment hostingEnvironment;

        public AdminMMCategoryController(IMainMenuCategoryService mainMenuCategoryService,
            IHostingEnvironment hostingEnvironment, ICategoryTypesService categoryTypesService)
        {
            this.mainMenuCategoryService = mainMenuCategoryService;
            this.hostingEnvironment = hostingEnvironment;
            this.categoryTypesService = categoryTypesService;
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public JsonResult LoadMainCategory()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            MainMenuCategoryRequestModel requestModel = new MainMenuCategoryRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false
            };
            //requestModel.SortColumnName = sortColumnName;
            var results = mainMenuCategoryService.GetMenus(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.MainMenuCategories });
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddMainCategory(MainMenuCategory category)
        {
            decimal Save = 0;
            if (category != null)
            {
                category.CreatedBy = HttpContext.Session.GetString("UserId");
                category.UpdatedBy = HttpContext.Session.GetString("UserId");
                category.CreatedDate = DateTime.Now;
                category.UpdatedDate = DateTime.Now;
                Save = mainMenuCategoryService.Add(category);
            }
            return Json(Save);
        }

        [ValidateAntiForgeryToken]
        public IActionResult FindMainCategory(decimal Id)
        {
            return Json(mainMenuCategoryService.FindById(Id));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateMainCategory(MainMenuCategory category)
        {
            int Result = 0;
            if (category != null)
            {
                category.UpdatedBy = HttpContext.Session.GetString("UserId");
                category.UpdatedDate = DateTime.Now;
                Result = mainMenuCategoryService.Update(category);
            }
            return Json(Result);
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteMainCategory(decimal Id)
        {
            return Json(mainMenuCategoryService.Delete(Id));
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [RequestSizeLimit(long.MaxValue)]
        [ValidateAntiForgeryToken]
        public JsonResult UploadCategoryBannerImage(decimal Id)
        {
            int Results = 0;
            var file = Request.Form.Files[0];
            Results = mainMenuCategoryService.UploadCategoryBannerImage(Id, file, HttpContext.Request.Host.Value + "/");
            return Json(Results);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [RequestSizeLimit(long.MaxValue)]
        [ValidateAntiForgeryToken]
        public JsonResult UploadCategoryFeatureImage(decimal Id)
        {
            int Results = 0;
            var file = Request.Form.Files[0];           
            Results = mainMenuCategoryService.UploadCategoryFeatureImage(Id, file, HttpContext.Request.Host.Value + "/");
            return Json(Results);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [RequestSizeLimit(long.MaxValue)]
        [ValidateAntiForgeryToken]
        public JsonResult UploadWebIcon(decimal Id)
        {
            int Results = 0;
            var file = Request.Form.Files[0];
            Results = mainMenuCategoryService.UploadWebIcons(Id, file, HttpContext.Request.Host.Value + "/");
            return Json(Results);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [RequestSizeLimit(long.MaxValue)]
        [ValidateAntiForgeryToken]
        public JsonResult UploadMobileIcon(decimal Id)
        {
            int Results = 0;
            var file = Request.Form.Files[0];
            Results = mainMenuCategoryService.UploadMobileIcons(Id, file, HttpContext.Request.Host.Value + "/");
            return Json(Results);
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetAllMainCategory()
        {
            return Json(mainMenuCategoryService.GetAll().ToList());
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetAllCategoryType()
        {
            return Json(categoryTypesService.GetAll());
        }
    }
}