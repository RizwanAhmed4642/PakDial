using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Interfaces.PakDialServices.Configuration;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{

    public class AdminSocialMediaModesController : Controller
    {
        #region Prop

        private readonly ISocialMediaModesService _ISocialMediaModesService;
        private readonly IHostingEnvironment _hostingEnvironment;

        #endregion

        #region Ctor

        public AdminSocialMediaModesController(ISocialMediaModesService ISocialMediaModesService, IHostingEnvironment hostingEnvironment)
        {

            _ISocialMediaModesService = ISocialMediaModesService;
            _hostingEnvironment = hostingEnvironment;

        }

        #endregion

        #region ActionMethod

        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public JsonResult LoadSocialMediaModes()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            SocialMediaModesRequestModel requestModel = new SocialMediaModesRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false
            };
            //requestModel.SortColumnName = sortColumnName;
            var results = _ISocialMediaModesService.GetSocialMediaModes(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.SocialMediaModes });
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddSocialMediaModes(SocialMediaModes socialMediaModes)
        {
            decimal Result = 0;
            if (socialMediaModes != null)
            {
                socialMediaModes.CreatedBy = HttpContext.Session.GetString("UserId");
                socialMediaModes.UpdatedBy = HttpContext.Session.GetString("UserId");
                socialMediaModes.CreatedDate = DateTime.Now;
                socialMediaModes.UpdatedDate = DateTime.Now;
                Result = _ISocialMediaModesService.Add(socialMediaModes);
            }
            return Json(Result);
        }


        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult EditSocialMediaModes(SocialMediaModes socialMediaModes)
        {


            socialMediaModes.UpdatedBy = HttpContext.Session.GetString("UserId");
            socialMediaModes.UpdatedDate = DateTime.Now;
            return Json(_ISocialMediaModesService.Update(socialMediaModes));

        }


        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteSocialMediaModes(int Id)
        {
            return Json(_ISocialMediaModesService.Delete(Id));
        }
       
        public JsonResult GetSocialMediaModesById(int Id)
        {
            return Json(_ISocialMediaModesService.GetById(Id));
        }
        /// <summary>
        /// Get All Listing Types from DataBase.
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        private JsonResult GetAllSocialMediaModes()
        {
            return Json(_ISocialMediaModesService.GetAll().ToList());
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [RequestSizeLimit(long.MaxValue)]
        [ValidateAntiForgeryToken]
        public JsonResult UploadProfileImage(decimal Id)
        {
            int Results = 0;
            var file = Request.Form.Files[0];
            Results = _ISocialMediaModesService.UploadIcons(Id, file, HttpContext.Request.Host.Value + "/");
            return Json(Results);
        }

        #endregion
    }
}