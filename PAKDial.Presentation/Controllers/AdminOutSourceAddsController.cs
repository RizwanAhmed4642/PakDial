using System;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAKDial.Domains.DomainModels;
using PAKDial.Domains.RequestModels;
using PAKDial.Interfaces.PakDialServices.IHomeLandingPageService;
using PAKDial.Presentation.Filters;

namespace PAKDial.Presentation.Controllers
{
    [Authorize]
    public class AdminOutSourceAddsController : Controller
    {
        private readonly IOutSourceAdvertismentService outSourceAdvertismentService;
        private readonly IHostingEnvironment hostingEnvironment;

        public AdminOutSourceAddsController(IOutSourceAdvertismentService outSourceAdvertismentService,
            IHostingEnvironment hostingEnvironment)
        {
            this.outSourceAdvertismentService = outSourceAdvertismentService;
            this.hostingEnvironment = hostingEnvironment;
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
        public JsonResult LoadOutSourceAdds()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            OutSourceAdvertismentRequestModel requestModel = new OutSourceAdvertismentRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false
            };
            //requestModel.SortColumnName = sortColumnName;
            var results = outSourceAdvertismentService.GetAdvertismentList(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.outSourceAdvertisments });
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddOutSourceAdds(OutSourceAdvertisment outSource)
        {
            decimal Save = 0;
            if (outSource != null)
            {
                outSource.CreatedBy = HttpContext.Session.GetString("UserId");
                outSource.UpdatedBy = HttpContext.Session.GetString("UserId");
                outSource.CreatedDate = DateTime.Now;
                outSource.UpdatedDate = DateTime.Now;
                Save = outSourceAdvertismentService.Add(outSource);
            }
            return Json(Save);

        }

        [ValidateAntiForgeryToken]
        public JsonResult GetOutSourceAddsById(decimal Id)
        {
            return Json(outSourceAdvertismentService.FindById(Id));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateOutSourceAdds(OutSourceAdvertisment outSource)
        {
            outSource.UpdatedBy = HttpContext.Session.GetString("UserId");
            outSource.UpdatedDate = DateTime.Now;
            return Json(outSourceAdvertismentService.Update(outSource));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteOutSourceAdds(decimal Id)
        {
            var oldImage = Path.Combine(new string[]
               {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","OutSourceAdvertisment",Id.ToString()
               });
            var MobileoldImage = Path.Combine(new string[]
               {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","MobOutSourceAdvertisment",Id.ToString()
               });

            var response = outSourceAdvertismentService.Delete(Id);
            if (Directory.Exists(oldImage) && response > 0)
            {
                Directory.Delete(oldImage, true);
            }
            if (Directory.Exists(MobileoldImage) && response > 0)
            {
                Directory.Delete(MobileoldImage, true);
            }
            return Json(response);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [RequestSizeLimit(long.MaxValue)]
        public JsonResult UploadOutSourceImage(decimal Id)
        {
            int Results = 0;
            var file = Request.Form.Files[0];
            var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
            var oldImage = Path.Combine(new string[]
                {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","OutSourceAdvertisment",Id.ToString()
                });
            if (Directory.Exists(oldImage))
            {
                Directory.Delete(oldImage, true);
            }
            var path = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","OutSourceAdvertisment",Id.ToString(),
                           Guid.NewGuid() + filename
                    });
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (FileStream fs = System.IO.File.Create(path))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            path = path.Replace(hostingEnvironment.WebRootPath, "").Replace("\\", "/");
            Results = outSourceAdvertismentService.UploadImages(Id, path);
            return Json(Results);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [RequestSizeLimit(long.MaxValue)]
        public JsonResult MobUploadOutSourceImage(decimal Id)
        {
            int Results = 0;
            var file = Request.Form.Files[0];
            var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
            var oldImage = Path.Combine(new string[]
                {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","MobOutSourceAdvertisment",Id.ToString()
                });
            if (Directory.Exists(oldImage))
            {
                Directory.Delete(oldImage, true);
            }
            var path = Path.Combine(new string[]
                    {
                           hostingEnvironment.WebRootPath,
                           "SystemImages","MobOutSourceAdvertisment",Id.ToString(),
                           Guid.NewGuid() + filename
                    });
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (FileStream fs = System.IO.File.Create(path))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            path = path.Replace(hostingEnvironment.WebRootPath, "").Replace("\\", "/");
            Results = outSourceAdvertismentService.MobUploadImages(Id, path);
            return Json(Results);
        }

    }
}