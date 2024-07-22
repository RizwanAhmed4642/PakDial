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
    public class AdminPaymentModesController : Controller
    {
        private readonly IPaymentModesService paymentModesService;

        public AdminPaymentModesController(IPaymentModesService paymentModesService)
        {
            this.paymentModesService = paymentModesService;
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public JsonResult LoadPaymentModes()
        {
            int draw = Convert.ToInt32(Request.Form["draw"]);
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            //string sortDirection = Request.Form["order[0][dir]"];

            PaymentModesRequestModel requestModel = new PaymentModesRequestModel
            {
                PageNo = start,
                PageSize = length,
                SearchString = searchValue,
                IsAsc = Request.Form["order[0][dir]"] == "asc" ? true : false
            };
            //requestModel.SortColumnName = sortColumnName;
            var results = paymentModesService.GetPaymentModes(requestModel);
            return Json(new { draw, recordsTotal = results.RowCount, recordsFiltered = results.RowCount, data = results.paymentModes });
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult AddPaymentModes(PaymentModes payment)
        {
            decimal Save = 0;
            if (payment != null)
            {
                payment.CreatedBy = HttpContext.Session.GetString("UserId");
                payment.UpdatedBy = HttpContext.Session.GetString("UserId");
                payment.CreatedDate = DateTime.Now;
                payment.UpdatedDate = DateTime.Now;
                Save = paymentModesService.Add(payment);
            }
            return Json(Save);

        }

        [ValidateAntiForgeryToken]
        public JsonResult GetPaymentModesById(decimal Id)
        {
            return Json(paymentModesService.FindById(Id));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult UpdatePaymentModes(PaymentModes payment)
        {
            payment.UpdatedBy = HttpContext.Session.GetString("UserId");
            payment.UpdatedDate = DateTime.Now;
            return Json(paymentModesService.Update(payment));
        }

        [ServiceFilter(typeof(CustomAuthorizationAttribute))]
        [ValidateAntiForgeryToken]
        public JsonResult DeletePaymentModes(decimal Id)
        {
            return Json(paymentModesService.Delete(Id));
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetAllPaymentModes()
        {
            return Json(paymentModesService.GetAll().ToList());
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [RequestSizeLimit(long.MaxValue)]
        [ValidateAntiForgeryToken]
        public JsonResult UploadIcon(decimal Id)
        {
            int Results = 0;
            var file = Request.Form.Files[0];
            Results = paymentModesService.UploadIcons(Id, file, HttpContext.Request.Host.Value + "/");
            return Json(Results);
        }
    }
}