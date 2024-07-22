using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.RequestModels;
using PAKDial.Interfaces.PakDialServices.ISearchesBehaviour;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminSearchBehaviourController : Controller
    {
        private readonly ISearchBehaviourService _searchBehaviourService;
        public AdminSearchBehaviourController(ISearchBehaviourService searchBehaviourService)
        {
            _searchBehaviourService = searchBehaviourService;
        }
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult LoadSearchBehaviour()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];

            SearchBehaviourRequestModel requestModel = new SearchBehaviourRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false
            };
            var results = _searchBehaviourService.GetSearchResults(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.SearchResults });
        }
    }
}