using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Interfaces.PakDialServices;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminRouteControlController : Controller
    {
        private readonly IRouteControlsService routeControlsService;
        public AdminRouteControlController(IRouteControlsService routeControlsService)
        {
            this.routeControlsService = routeControlsService;
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        public JsonResult LoadRoutes()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            RouteControlsRequestModel requestModel = new RouteControlsRequestModel();
            requestModel.PageNo = start;
            requestModel.PageSize = length;
            requestModel.SearchString = searchValue;
            requestModel.IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false;
            //requestModel.SortColumnName = sortColumnName;
            var results = routeControlsService.GetRouteControls(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.RouteControl });
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddRoute(RouteControls route)
        {
            int Save = 0;
            if (route != null)
            {
                route.CreatedBy = HttpContext.Session.GetString("UserId");
                route.UpdatedBy = HttpContext.Session.GetString("UserId");
                route.CreatedDate = DateTime.Now;
                route.UpdatedDate = DateTime.Now;
                Save = routeControlsService.Add(route);
            }
            return Json(Save);

        }
        [ValidateAntiForgeryToken]
        public JsonResult GetRouteById(decimal Id)
        {
            return Json(routeControlsService.FindById(Id));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateRoute(RouteControls route)
        {
            route.UpdatedBy = HttpContext.Session.GetString("UserId");
            route.UpdatedDate = DateTime.Now;
            return Json(routeControlsService.Update(route));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteRoute(decimal Id)
        {
            return Json(routeControlsService.Delete(Id));
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetAllRoutes()
        {
            return Json(routeControlsService.GetAll().ToList());
        }
    }
}