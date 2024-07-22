using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminSpecialCategoryController : Controller
    {
        private readonly IHomeSecMainMenuCatService homeSecMainMenuCatService;

        public AdminSpecialCategoryController(IHomeSecMainMenuCatService homeSecMainMenuCatService)
        {
            this.homeSecMainMenuCatService = homeSecMainMenuCatService;
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public JsonResult LoadSpecialCategory()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];

            HomeSecMainMenuCatRequestModel requestModel = new HomeSecMainMenuCatRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false
            };
            //requestModel.SortColumnName = sortColumnName;
            var results = homeSecMainMenuCatService.GetHomeSecMainMenu(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.VHomeSecMainMenuCats });
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddSpecialCategory(HomeSecMainMenuCat homeSecMain)
        {
            int Save = 0;
            if (homeSecMain != null)
            {
                Save = homeSecMainMenuCatService.Add(homeSecMain);
            }
            return Json(Save);
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteSpecialCategory(decimal MainMenuCatId, decimal HomeSecCatId)
        {
            return Json(homeSecMainMenuCatService.Delete(new HomeSecMainMenuCat { MainMenuCatId = MainMenuCatId, HomeSecCatId = HomeSecCatId }));
        }
    }
}