using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels.Configuration;
using PAKDial.Interfaces.PakDialServices.Configuration;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminVerificationTypesController : Controller
    {
        #region Prop

        private readonly IVerificationTypesService _IVerificationTypesService;
        private readonly IHostingEnvironment _hostingEnvironment;

        #endregion

        #region Ctor

        public AdminVerificationTypesController(IVerificationTypesService IVerificationTypesService, IHostingEnvironment hostingEnvironment)
        {
            _IVerificationTypesService = IVerificationTypesService;
            _hostingEnvironment = hostingEnvironment;
        }

        #endregion

        #region Actin Method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Load Verfication Types From DataBase.
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public JsonResult LoadVerificationTypes()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            VerificationTypesRequestModel requestModel = new VerificationTypesRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false
            };
            //requestModel.SortColumnName = sortColumnName;
            var results = _IVerificationTypesService.GetVerificationTypes(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.VerificationTypes });
        }

        /// <summary>
        /// Add new Verfication Types
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddVerificationTypes(VerificationTypes verificationTypes)
        {
            decimal Result = 0;
            if (verificationTypes != null)
            {
                verificationTypes.CreatedBy = HttpContext.Session.GetString("UserId");
                verificationTypes.UpdatedBy = HttpContext.Session.GetString("UserId");
                verificationTypes.CreatedDate = DateTime.Now;
                verificationTypes.UpdatedDate = DateTime.Now;
                Result = _IVerificationTypesService.Add(verificationTypes);
            }
            return Json(Result);
        }

        /// <summary>
        /// Update Existing verificationTypes Record.
        /// </summary>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult EditVerificationTypes(VerificationTypes verificationTypes)
        {


            verificationTypes.UpdatedBy = HttpContext.Session.GetString("UserId");
            verificationTypes.UpdatedDate = DateTime.Now;
            return Json(_IVerificationTypesService.Update(verificationTypes));

        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteVerificationTypes(int Id)
        {
            return Json(_IVerificationTypesService.Delete(Id));
        }

       
        public JsonResult GetVerificationTypesById(int Id)
        {
            return Json(_IVerificationTypesService.GetById(Id));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        private JsonResult GetAllVerificationTypes()
        {
            return Json(_IVerificationTypesService.GetAll().ToList());
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [RequestSizeLimit(long.MaxValue)]
        public JsonResult UploadProfileImage(decimal Id)
        {
            int Results = 0;
            var file = Request.Form.Files[0];
            var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
            var Emppath = Path.Combine(new string[]
                 {
                           _hostingEnvironment.WebRootPath,
                           "SystemImages","VerificationType",Id.ToString()
                 });
            if (Directory.Exists(Emppath))
            {
                Directory.Delete(Emppath, true);
            }
            var path = Path.Combine(new string[]
                    {
                           _hostingEnvironment.WebRootPath,
                           "SystemImages","VerificationTypes",Id.ToString(),
                           Guid.NewGuid() + filename
                    });

            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (FileStream fs = System.IO.File.Create(path))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            path = path.Replace(_hostingEnvironment.WebRootPath, "").Replace("\\", "/");
            string AbsolutePath = ((Microsoft.AspNetCore.Http.Internal.DefaultHttpRequest)HttpContext.Request).Host.Value + "/" + path;
            Results = _IVerificationTypesService.ImageUpdate(Id, path, AbsolutePath);
            return Json(Results);
        }

        #endregion

    }
}