using System;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Domains.ViewModels;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    public class AdminSubMenuCategoryController : Controller
    {
        private readonly ISubMenuCategoryService subMenuCategoryService;
        private readonly IHostingEnvironment hostingEnvironment;

        public AdminSubMenuCategoryController(ISubMenuCategoryService subMenuCategoryService,
            IHostingEnvironment hostingEnvironment)
        {
            this.subMenuCategoryService = subMenuCategoryService;
            this.hostingEnvironment = hostingEnvironment;
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public JsonResult LoadSubCategory()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            SubMenuCategoryRequestModel requestModel = new SubMenuCategoryRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false
            };
            //requestModel.SortColumnName = sortColumnName;
            var results = subMenuCategoryService.GetSubMenus(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.SubMenuCategories });
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddSubCategory(SubMenuCategory category)
        {
            decimal Save = 0;
            if (category != null)
            {
                category.CreatedBy = HttpContext.Session.GetString("UserId");
                category.UpdatedBy = HttpContext.Session.GetString("UserId");
                category.CreatedDate = DateTime.Now;
                category.UpdatedDate = DateTime.Now;
                Save = subMenuCategoryService.Add(category);
            }
            return Json(Save);
        }

        [ValidateAntiForgeryToken]
        public IActionResult FindSubCategory(decimal Id)
        {
            return Json(subMenuCategoryService.FindById(Id));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSubCategory(SubMenuCategory category)
        {
            int Result = 0;
            if (category != null)
            {
                category.UpdatedBy = HttpContext.Session.GetString("UserId");
                category.UpdatedDate = DateTime.Now;
                Result = subMenuCategoryService.Update(category);
            }
            return Json(Result);
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteSubCategory(decimal Id)
        {
            var Bannerpath = Path.Combine(new string[]
                   {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","SubCategory","BannerImages",Id.ToString()
                   });
            var Featurepath = Path.Combine(new string[]
                   {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","SubCategory","FeatureImages",Id.ToString()
                   });

            var response = subMenuCategoryService.Delete(Id);
            if (response == 1)
            {
                if (Directory.Exists(Bannerpath))
                {
                    Directory.Delete(Bannerpath, true);
                }
                if (Directory.Exists(Featurepath))
                {
                    Directory.Delete(Featurepath, true);

                }
            }
            return Json(response);
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult IsPopularCategory(decimal Id, string name)
        {
            return Json(subMenuCategoryService.IsPopularCategory(Id, name));
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [RequestSizeLimit(long.MaxValue)]
        [ValidateAntiForgeryToken]
        public JsonResult UploadSubCategoryBannerImage(decimal Id)
        {
            int Results = 0;
            var file = Request.Form.Files[0];
            Results = subMenuCategoryService.UploadCategoryBannerImage(Id, file, HttpContext.Request.Host.Value + "/");
            return Json(Results);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [RequestSizeLimit(long.MaxValue)]
        [ValidateAntiForgeryToken]
        public JsonResult UploadSubCategoryFeatureImage(decimal Id)
        {
            int Results = 0;
            var file = Request.Form.Files[0];
            Results = subMenuCategoryService.UploadCategoryFeatureImage(Id, file, HttpContext.Request.Host.Value + "/");
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
            Results = subMenuCategoryService.UploadWebIcon(Id, file, HttpContext.Request.Host.Value + "/");
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
            Results = subMenuCategoryService.UploadMobileIcon(Id, file, HttpContext.Request.Host.Value + "/");
            return Json(Results);
        }

        [ValidateAntiForgeryToken]
        public JsonResult LoadListSubCategory(string search, string pageNo, string pageSize, string CatId, string MainCatId)
        {
            if (!string.IsNullOrEmpty(MainCatId))
            {
                SubCategoryRequestModel requestModel = new SubCategoryRequestModel
                {
                    PageNo = Convert.ToInt32(pageNo),
                    PageSize = Convert.ToInt32(pageSize),
                    SearchString = search,
                    SubCatId = Convert.ToDecimal(CatId),
                    MainCatId = Convert.ToDecimal(MainCatId),
                };
                var results = subMenuCategoryService.GetSubMenusList(requestModel);
                return Json(results);
            }
            else
                return Json(null);

        }

        [ValidateAntiForgeryToken]
        public JsonResult LoadEditSubCategory(string CatId)
        {
            var results = subMenuCategoryService.FindById(Convert.ToDecimal(CatId));

            return Json(new VMSubCategoryValuePair { id = results.TrackIds, text = results.TrackNames });
        }
    }
}